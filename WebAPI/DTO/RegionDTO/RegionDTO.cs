using WebAPI.Domain.Models;

namespace WebAPI.DTO.RegionDTO ;

public class RegionDto
{
    public required string Id { get; set; }
    public required string Name { get; set; } 
    public List<League> Leagues { get; set; } = new List<League>();
}