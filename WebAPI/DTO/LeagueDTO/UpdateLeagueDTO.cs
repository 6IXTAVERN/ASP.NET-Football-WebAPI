using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.DTO.LeagueDTO;

public class UpdateLeagueDto
{
    [Required]
    [MaxLength(50, ErrorMessage = "Name cannot be over 50 characters")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [ForeignKey("RegionId")]
    public string RegionId { get; set; }
}