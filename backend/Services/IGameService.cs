using backend.Helpers;
using backend.Models;

namespace backend.Services;

public interface IGameService
{
    IQueryable<Game>? GetGames();
    Task<PaginatedList<Game>> GetGames(GameFilterDto gameDTO);
    Task<Game?> GetGameById(int id);
}
