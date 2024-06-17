using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Domain.Models;

namespace WebAPI.DTO.LeagueDTO;

public class LeagueDto
{
    public required string Id { get; set; }
    public required string Name { get; set; } 
    [ForeignKey("RegionId")]
    public string? RegionId { get; set; }
    public Region? Region{ get; set; }
    public List<Team> Teams { get; set; } = new List<Team>();
}