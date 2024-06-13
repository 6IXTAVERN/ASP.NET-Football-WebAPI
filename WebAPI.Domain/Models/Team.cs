namespace WebAPI.Domain.Models ;

public class Team
{
    public string Id { get; set; }
    public string Name { get; set; }
    public Manager? Manager { get; set; }
    public ICollection<Player> Players { get; set; }
    public string LeagueId { get; set; }
    public League League { get; set; }
    
    public Team()
    {
        Id = Guid.NewGuid().ToString();
        Name = "";
        Manager = null;
        Players = new List<Player>();
        LeagueId = "";
        League = new League();
    }
}