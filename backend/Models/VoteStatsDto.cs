namespace backend.Models;

public class VoteStatsDto
{
    public Dictionary<int, int> NumberOfVotesPerRating { get; set; }
    public double AverageRating { get; set; }
    public int MostVotesForARating { get; set; }

    public VoteStatsDto(Dictionary<int, int> numberOfVotesPerRating, double averageRating, int mostVotesForARating)
    {
        NumberOfVotesPerRating = numberOfVotesPerRating;
        AverageRating = averageRating;
        MostVotesForARating = mostVotesForARating;
    }
}
