using backend.Data;
using backend.Data.Dto;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VoteController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public VoteController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Vote([FromBody] VoteDto voteDto)
    {
        if (IsVoteInvalid(voteDto.Rating))
        {
            return StatusCode(
                StatusCodes.Status400BadRequest,
                new { Status = "Error", Message = "Invalid vote." }
            );
        }

        var username = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users
            .Where(user => user.UserName == username)
            .Include(user => user.Ratings)
            .FirstAsync();
        var game = await _context.Games
            .Where(game => game.Id == voteDto.GameId)
            .Include(game => game.Votes)
            .FirstAsync();
        var vote = await _context.Votes
            .Where(vote => vote.Game.Id == voteDto.GameId)
            .Where(vote => vote.User.Id == user.Id)
            .FirstOrDefaultAsync();

        if (vote != null)
        {
            _context.Votes.Remove(vote);
        }

        if (voteDto.Rating != 0)
        {
            vote = new Vote
            {
                Rating = voteDto.Rating,
                User = user,
                Game = game,
            };

            _context.Votes.Add(vote);
        }

        await _context.SaveChangesAsync();

        return Ok();
    }

    private static bool IsVoteInvalid(int rating)
    {
        return rating < 0 || rating > 10;
    }
}
