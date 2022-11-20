using backend.Helpers;
using backend.Models;

namespace backend.Data.Dto;

public class GetGamesResponse
{
    public PaginatedList<Game>? Games { get; set; }
    public string Search { get; set; }
    public string Sort { get; set; }
    public PaginationDto? Pagination { get; set; }

    public GetGamesResponse(PaginatedList<Game>? games, string search, string sort, PaginationDto? pagination)
    {
        Games = games;
        Search = search;
        Sort = sort;
        Pagination = pagination;
    }
}
