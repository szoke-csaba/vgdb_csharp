using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public enum UserListType
{
    [Display(Name = "Plan to play")]
    PlanToPlay = 1,
    Playing = 2,
    Played = 3,
    [Display(Name = "On hold")]
    OnHold = 4,
    Dropped = 5
}
