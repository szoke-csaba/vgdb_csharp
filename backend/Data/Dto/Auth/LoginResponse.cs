namespace backend.Data.Dto.Auth;

public class LoginResponse
{
    public string Id { get; set; }
    public string Token { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public IList<string> Roles { get; set; }

    public LoginResponse(string id, string token, string username, string email, IList<string> roles)
    {
        Id = id;
        Token = token;
        Username = username;
        Email = email;
        Roles = roles;
    }
}
