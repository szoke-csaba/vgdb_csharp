using backend.Models;

namespace backend.Repositories;

public interface IGameRepository
{
    public IQueryable<Game>? GetGames();
    public Task<Game?> GetGameById(int id);
}

