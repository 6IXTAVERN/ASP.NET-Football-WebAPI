using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTO.AccountDTO ;

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    public bool RememberMe { get; set; }
}