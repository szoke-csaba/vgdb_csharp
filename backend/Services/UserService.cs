using backend.Data.Dto.Auth;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Identity;

namespace backend.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
    {
        _userRepository = userRepository;
        _userManager = userManager;
    }

    public async Task<ApplicationUser?> GetUserById(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task<ApplicationUser> GetUserByUsernameWithRatingsAndLists(string username)
    {
        return await _userRepository.GetUserByUsernameWithRatingsAndLists(username);
    }

    public async Task<LoginResponse> GetUserResponse(ApplicationUser user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        return new LoginResponse(user.Id, string.Empty, user.UserName, user.Email, userRoles);
    }
}
