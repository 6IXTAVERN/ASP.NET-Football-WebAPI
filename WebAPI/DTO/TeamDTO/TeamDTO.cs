using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Domain.Models;

namespace WebAPI.DTO.TeamDTO ;

public class TeamDto
{
    public required string Id { get; set; }
    public required string Name { get; set; } 
    [ForeignKey("LeagueId")]
    public required string LeagueId { get; set; }
    public required League League { get; set; }
    public List<Player> Players { get; set; } = new List<Player>();
    public Manager? Manager { get; set; }
}