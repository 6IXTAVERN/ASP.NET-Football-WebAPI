using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Domain.Models;

namespace WebAPI.DTO.LeagueDTO ;

public class LeagueDto
{
    public string Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [ForeignKey("RegionId")]
    public string? RegionId { get; set; }
    public Region? Region{ get; set; }
    public List<Team> Teams { get; set; } = new List<Team>();
}