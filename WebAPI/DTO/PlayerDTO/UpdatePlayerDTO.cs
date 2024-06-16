using System.ComponentModel.DataAnnotations;

namespace WebAPI.Domain.DTO.Player ;

public class UpdatePlayerDto
{
    [Required]
    [MaxLength(50, ErrorMessage = "FullName cannot be over 50 characters")]
    public string FullName { get; set; } = string.Empty;

    [Required]
    public string Nationality { get; set; } = string.Empty;

    [Required]
    public string Position { get; set; } = string.Empty;

    public string? TeamId { get; set; }
}