using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTO.AccountDTO ;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}