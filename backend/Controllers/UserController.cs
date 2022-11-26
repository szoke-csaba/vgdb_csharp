using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var user = await _userService.GetUserById(id);

        if (user == null)
        {
            return StatusCode(
                StatusCodes.Status404NotFound,
            new { Status = "Error", Message = "No user found." }
            );
        }

        return Ok(await _userService.GetUserResponse(user));
    }
}
