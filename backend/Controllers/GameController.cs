using backend.Data;
using backend.Helpers;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private const int _pageSize = 6;

    private readonly ApplicationDbContext _context;

    public GameController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] GameFilterDto gameDTO)
    {
        if (_context.Games?.Count() == 0)
        {
            return StatusCode(
                StatusCodes.Status404NotFound,
                new { Status = "Error", Message = "No games found." }
            );
        }

        var games = from g in _context.Games
                    select g;

        if (!string.IsNullOrEmpty(gameDTO.Search))
        {
            games = games.Where(game => game.Title.Contains(gameDTO.Search));
        }

        games = gameDTO.Sort switch
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

        var gameList = await PaginatedList<Game>.CreateAsync(games.AsNoTracking(), gameDTO.Page, _pageSize);
        var pagination = new PaginationDto(gameList.CurrentPage, gameList.TotalPages, gameList.PageSize, gameList.TotalCount,
                gameList.PageItemsFrom, gameList.PageItemsTo, gameList.HasPrevious, gameList.HasNext, gameList.PreviousPage, gameList.NextPage);

        var response = new Dictionary<string, object>()
        {
            { "games", gameList },
            { "search", gameDTO.Search },
            { "sort", gameDTO.Sort },
            { "pagination", pagination },
        };

        return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        if (_context.Games == null)
        {
            return StatusCode(
                StatusCodes.Status404NotFound,
                new { Status = "Error", Message = "No game found." }
            );
        }

        var game = await _context.Games
            .Where(game => game.Id == id)
            .Include(game => game.Tags)
            .Include(game => game.Genres)
            .Include(game => game.Platforms)
            .Include(game => game.Developers)
            .Include(game => game.Publishers)
            .Include(game => game.Votes)
            .Include(game => game.Lists)
            .FirstOrDefaultAsync();

        if (game == null)
        {
            return StatusCode(
                StatusCodes.Status404NotFound,
                new { Status = "Error", Message = "No game found." }
            );
        }

        var ratings = new Dictionary<string, object>()
        {
            { "numberOfVotesPerRating", game.NumberOfVotesPerRating() },
            { "averageRating", game.AverageRating() },
            { "mostVotesForARating", game.MostVotesForARating() }
        };

        var response = new Dictionary<string, object>
        {
            { "game", game },
            { "voteStats", ratings }
        };

        var username = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (username != null)
        {
            var user = await _context.Users
                .Where(user => user.UserName == username)
                .Include(user => user.Ratings)
                .Include(user => user.Lists)
                .FirstAsync();

            response.Add("userRating", user.GameRating(id));
            response.Add("userListType", user.GameUserListType(id));
        }

        return Ok(response);
    }
}
