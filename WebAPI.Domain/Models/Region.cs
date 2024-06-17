namespace WebAPI.Domain.Models ;

public class Region
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Name { get; set; }
    public ICollection<League> Leagues { get; set; } = new List<League>();
}