using backend.Data.Dto.Auth;
using backend.Models;

namespace backend.Services;

public interface IUserService
{
    Task<ApplicationUser> GetUserByUsernameWithRatingsAndLists(string username);
    Task<ApplicationUser?> GetUserById(string id);
    Task<LoginResponse> GetUserResponse(ApplicationUser user);
}
