namespace WebAPI.Domain.Models ;

public class League
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public ICollection<Team> Teams { get; set; }
    public string RegionId { get; set; }
    public Region Region { get; set; }

    public League()
    {
        Id = Guid.NewGuid().ToString();
        Name = "";
        Country = "";
        Teams = new List<Team>();
        RegionId = "";
        Region = new Region();
    }
}