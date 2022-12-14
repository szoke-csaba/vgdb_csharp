using backend.Models;

namespace backend.Data.Dto;

public class GetGameResponse
{
    public Game Game { get; set; }
    public VoteStatsDto VoteStats { get; set; }
    public int UserRating { get; set; }
    public int UserListType { get; set; }
}
