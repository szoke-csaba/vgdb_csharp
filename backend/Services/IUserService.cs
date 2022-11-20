using backend.Models;

namespace backend.Services;

public interface IUserService
{
    Task<ApplicationUser> GetUserByUsernameWithRatingsAndLists(string username);
}
