﻿using backend.Data;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IGameService _gameService;

    public GameController(ApplicationDbContext context, IGameService gameService)
    {
        _context = context;
        _gameService = gameService;
    }

    [HttpGet]
    public async Task<IActionResult> GetGames([FromQuery] GameFilterDto gameDto)
    {
        if (_gameService.GetGames()?.Count() == 0)
        {
            return StatusCode(
                StatusCodes.Status404NotFound,
                new { Status = "Error", Message = "No games found." }
            );
        }

        var gameList = await _gameService.GetGames(gameDto);
        var pagination = new PaginationDto(gameList.CurrentPage, gameList.TotalPages, gameList.PageSize, gameList.TotalCount,
                gameList.PageItemsFrom, gameList.PageItemsTo, gameList.HasPrevious, gameList.HasNext, gameList.PreviousPage, gameList.NextPage);

        return Ok(new GetGamesResponse(gameList, gameDto.Search, gameDto.Sort, pagination));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetGame(int id)
    {
        var game = await _gameService.GetGameById(id);

        if (game == null)
        {
            return StatusCode(
                StatusCodes.Status404NotFound,
                new { Status = "Error", Message = "No game found." }
            );
        }

        var voteStats = new VoteStatsDto(game.NumberOfVotesPerRating(), game.AverageRating(), game.MostVotesForARating());
        var response = new GetGameResponse
        {
            Game = game,
            VoteStats = voteStats
        };

        var username = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (username != null)
        {
            var user = await _context.Users
                .Where(user => user.UserName == username)
                .Include(user => user.Ratings)
                .Include(user => user.Lists)
                .FirstAsync();

            response.UserRating = user.GameRating(id);
            response.UserListType = user.GameUserListType(id);
        }

        return Ok(response);
    }
}
