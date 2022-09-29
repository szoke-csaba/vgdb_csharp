using Microsoft.AspNetCore.Identity;

namespace backend.Models;

public class ApplicationUser : IdentityUser
{
    public ICollection<Vote> Ratings { get; set; }

    public ICollection<UserList> Lists { get; set; }

    public int GameRating(int gameId)
    {
        return Ratings?.FirstOrDefault(vote => vote.Game.Id == gameId)?.Rating ?? 0;
    }

    public int GameUserListType(int gameId)
    {
        if (Lists == null)
        {
            return 0;
        }

        var list = Lists.FirstOrDefault(list => list.Game?.Id == gameId);

        if (list == null)
        {
            return 0;
        }

        return (int) list.Type;
    }
}
