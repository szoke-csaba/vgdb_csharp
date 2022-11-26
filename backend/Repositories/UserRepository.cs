using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApplicationUser> GetUserByUsernameWithRatingsAndLists(string username)
    {
        return await _context.Users
                .Where(user => user.UserName == username)
                .Include(user => user.Ratings)
                .Include(user => user.Lists)
                .FirstAsync();
    }
}
