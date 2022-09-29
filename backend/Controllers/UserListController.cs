using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserListController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UserListController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> ChangeList([FromBody] UserListDto userListDto)
    {
        if (IsListTypeInvalid(userListDto.ListType))
        {
            return StatusCode(
                StatusCodes.Status400BadRequest,
                new { Status = "Error", Message = "Invalid list type." }
            );
        }

        var username = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users
            .Where(user => user.UserName == username)
            .FirstAsync();
        var game = await _context.Games
            .Where(game => game.Id == userListDto.GameId)
            .FirstAsync();
        var list = await _context.UserLists
            .Where(vote => vote.Game.Id == userListDto.GameId)
            .Where(vote => vote.User.Id == user.Id)
            .FirstOrDefaultAsync();

        if (list != null)
        {
            _context.UserLists.Remove(list);
        }

        if (userListDto.ListType != 0)
        {
            list = new UserList
            {
                Type = (UserListType) userListDto.ListType,
                User = user,
                Game = game,
            };

            _context.UserLists.Add(list);
        }

        await _context.SaveChangesAsync();

        return Ok();
    }

    private static bool IsListTypeInvalid(int listType)
    {
        return !Enum.IsDefined(typeof(UserListType), listType) && listType != 0;
    }
}
