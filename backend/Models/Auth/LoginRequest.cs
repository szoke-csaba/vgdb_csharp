using System.ComponentModel.DataAnnotations;

namespace backend.Models.Auth;

public class LoginRequest
{
    [Required(ErrorMessage = "Username/Email is required.")]
    public string? UsernameOrEmail { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public string? Password { get; set; }
}
