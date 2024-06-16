using WebAPI.Domain.Models;

namespace WebAPI.DTO.RegionDTO ;

public class RegionDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<Team> Teams { get; set; } = new List<Team>();
}