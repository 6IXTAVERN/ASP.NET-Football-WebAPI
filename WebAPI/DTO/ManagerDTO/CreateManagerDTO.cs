using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.DTO.ManagerDTO ;

public class CreateManagerDto
{
    [Required]
    [MaxLength(50, ErrorMessage = "FullName cannot be over 50 characters")]
    public string FullName { get; set; } = string.Empty;

    [Required]
    public string Nationality { get; set; } = string.Empty;

    [ForeignKey("TeamId")]
    public string? TeamId { get; set; }
}