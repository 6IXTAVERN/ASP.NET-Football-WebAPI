namespace WebAPI.DTO.AccountDTO ;

public class AuthResultDto
{
    public string Email { get; set; }
    public string AuthToken { get; set; }
    public IEnumerable<string> Roles { get; set; }
    
    public AuthResultDto(string email, string authToken, IEnumerable<string> roles)
    {
        Email = email;
        AuthToken = authToken;
        Roles = roles;
    }
}