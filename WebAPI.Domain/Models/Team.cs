namespace WebAPI.Domain.Models ;

public class Team
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Name { get; set; }
    public ICollection<Player> Players { get; set; } = new List<Player>();
    public Manager? Manager { get; set; }
    public required string LeagueId { get; set; }
    public required League League { get; set; }
}