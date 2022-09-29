using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Game
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Title { get; set; }

    public DateTime ReleaseDate { get; set; }

    public ICollection<Tag> Tags { get; set; }

    public ICollection<Genre> Genres { get; set; }

    public ICollection<Platform> Platforms { get; set; }

    public ICollection<Developer> Developers { get; set; }

    public ICollection<Publisher> Publishers { get; set; }

    public ICollection<Vote> Votes { get; set; }

    public ICollection<UserList> Lists { get; set; }

    public double AverageRating()
    {
        return Votes.Average(vote => vote.Rating);
    }

    public int MostVotesForARating()
    {
        if (Votes.Count == 0)
        {
            return 0;
        }

        return Votes
            .GroupBy(vote => vote.Rating)
            .OrderBy(vote => vote.Count())
            .Last().ToList().Count;
    }

    public Dictionary<int, int> NumberOfVotesPerRating()
    {
        var dictionary = Votes
            .GroupBy(
                vote => vote.Rating,
                (rating, vote) => new
                {
                    Key = rating,
                    Count = vote.Count()
                }
            )
            .ToDictionary(
                vote => vote.Key,
                vote => vote.Count
            );

        return FillMissingRatingsWithZero(dictionary);
    }

    private Dictionary<int, int> FillMissingRatingsWithZero(Dictionary<int, int> dictionary)
    {
        var newDictionary = new Dictionary<int, int>();

        for (int i = 10; i >= 1; i--)
        {
            newDictionary.Add(i, dictionary.GetValueOrDefault(i, 0));
        }

        return newDictionary;
    }
}
