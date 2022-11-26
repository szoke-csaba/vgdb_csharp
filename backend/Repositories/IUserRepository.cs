using backend.Models;

namespace backend.Repositories;

public interface IUserRepository
{
    public Task<ApplicationUser> GetUserByUsernameWithRatingsAndLists(string username);
}

