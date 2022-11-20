using System.ComponentModel.DataAnnotations;

namespace backend.Data.Dto.Auth;

public class RegisterRequest
{
    [Required(ErrorMessage = "Username is required.")]
    public string? Username { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email is required.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Password confirm is required.")]
    [Compare("Password", ErrorMessage = "Passwords must match.")]
    public string? PasswordConfirm { get; set; }
}
