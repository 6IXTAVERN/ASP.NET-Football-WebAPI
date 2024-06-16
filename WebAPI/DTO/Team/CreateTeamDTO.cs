using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Domain.DTO.Team ;

public class CreateTeamDto
{
    [Required]
    [MaxLength(50, ErrorMessage = "Name cannot be over 50 characters")]
    public string Name { get; set; } = string.Empty;
    [Required]
    [ForeignKey("LeagueId")]
    public int? LeagueId { get; set; }
}