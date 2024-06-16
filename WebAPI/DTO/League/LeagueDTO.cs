using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Domain.Models;

namespace WebAPI.DTO.League ;

public class LeagueDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [ForeignKey("RegionId")]
    public int? RegionId { get; set; }
    public Region? Region{ get; set; }
    public List<Team> Teams { get; set; } = new List<Team>();
}