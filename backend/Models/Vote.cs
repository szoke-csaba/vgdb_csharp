using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Vote
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int Rating { get; set; }

    public Game Game { get; set; }

    public ApplicationUser User { get; set; }
}
