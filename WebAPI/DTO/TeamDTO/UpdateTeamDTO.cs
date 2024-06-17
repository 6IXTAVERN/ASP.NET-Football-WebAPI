using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.DTO.TeamDTO ;

public class UpdateTeamDto
{
    [Required]
    [MaxLength(50, ErrorMessage = "Name cannot be over 50 characters")]
    public string Name { get; set; } = string.Empty;
    [Required]
    [ForeignKey("LeagueId")]
    public required string LeagueId { get; set; }
}