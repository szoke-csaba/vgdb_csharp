namespace backend.Models;

public class GameFilterDto
{
    public string Sort { get; set; } = string.Empty;
    public string Search { get; set; } = string.Empty;
    public int Page { get; set; } = 1;
}
