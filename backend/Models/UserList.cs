using System.ComponentModel.DataAnnotations;
using backend.Data;

namespace backend.Models;

public class UserList
{
    [Key]
    public int Id { get; set; }

    public UserListType Type { get; set; }

    public Game Game { get; set; }

    public ApplicationUser User { get; set; }
}
