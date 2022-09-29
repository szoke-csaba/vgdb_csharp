namespace backend.Models;

public class GameFilterDto
{
    public string? Sort { get; set; } = "";
    public string? Search { get; set; } = "";
    public int Page { get; set; } = 1;
}
