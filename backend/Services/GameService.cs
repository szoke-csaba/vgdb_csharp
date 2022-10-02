using backend.Data;
using backend.Helpers;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class GameService : IGameService
{
    private const int PageSize = 6;

    private readonly ApplicationDbContext _context;

    public GameService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<Game>? GetGames()
    {
        return _context.Games;
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

        return await PaginatedList<Game>.CreateAsync(games.AsNoTracking(), gameDto.Page, PageSize);
    }

    public async Task<Game?> GetGameById(int id)
    {
        return await _context.Games
            .Where(game => game.Id == id)
            .Include(game => game.Tags)
            .Include(game => game.Genres)
            .Include(game => game.Platforms)
            .Include(game => game.Developers)
            .Include(game => game.Publishers)
            .Include(game => game.Votes)
            .Include(game => game.Lists)
            .FirstOrDefaultAsync();
    }
}
