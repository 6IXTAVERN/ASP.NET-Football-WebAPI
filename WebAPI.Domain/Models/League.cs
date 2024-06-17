namespace WebAPI.Domain.Models;

public class League
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Name { get; set; }
    public ICollection<Team> Teams { get; set; } = new List<Team>();
    public required string RegionId { get; set; }
    public required Region Region { get; set; }
}