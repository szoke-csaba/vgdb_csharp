using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class GameRepository : IGameRepository
{
    private readonly ApplicationDbContext _context;

    public GameRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<Game>? GetGames()
    {
        return _context.Games;
    }

    public async Task<Game?> GetGameById(int id)
    {
        return await GetGames()
            .Where(game => game.Id == id)
            .Include(game => game.Tags)
            .Include(game => game.Genres)
            .Include(game => game.Platforms)
            .Include(game => game.Developers)
            .Include(game => game.Publishers)
            .Include(game => game.Votes)
            .Include(game => game.Lists)
            .FirstOrDefaultAsync();
    }
}
