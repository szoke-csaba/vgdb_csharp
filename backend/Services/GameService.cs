using backend.Data.Dto;
using backend.Helpers;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class GameService : IGameService
{
    private readonly int _pageSize;
    private readonly IGameRepository _gameRepository;

    public GameService(IGameRepository gameRepository, [FromServices] IConfiguration config)
    {
        _gameRepository = gameRepository;
        _pageSize = config.GetValue<int>("GamesPageSize");
    }

    public IQueryable<Game>? GetGames()
    {
        return _gameRepository.GetGames();
    }

    public async Task<PaginatedList<Game>> GetGames(GameFilterDto gameDto)
    {
        var games = from g in GetGames()
                    select g;

        if (!string.IsNullOrEmpty(gameDto.Search))
        {
            games = games.Where(game => game.Title.Contains(gameDto.Search));
        }

        games = gameDto.Sort switch
        {
            "oldest" => games.OrderBy(game => game.Id),
            "titleAsc" => games.OrderBy(game => game.Title),
            "titleDesc" => games.OrderByDescending(game => game.Title),
            "releaseYearAsc" => games.OrderBy(game => game.ReleaseDate),
            "releaseYearDesc" => games.OrderByDescending(game => game.ReleaseDate),
            "avgRatingAsc" => games.OrderBy(game => game.Votes.Average(vote => vote.Rating)),
            "avgRatingDesc" => games.OrderByDescending(game => game.Votes.Average(vote => vote.Rating)),
            _ => games.OrderByDescending(game => game.Id),
        };

        return await PaginatedList<Game>.CreateAsync(games.AsNoTracking(), gameDto.Page, _pageSize);
    }

    public async Task<Game?> GetGameById(int id)
    {
        return await _gameRepository.GetGameById(id);
    }
}
