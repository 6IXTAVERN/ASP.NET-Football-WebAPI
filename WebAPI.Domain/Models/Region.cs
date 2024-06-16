namespace WebAPI.Domain.Models ;

public class Region
{
    public string Id { get; set; }
    public string Name { get; set; }
    public ICollection<League> Leagues { get; set; }

    public Region()
    {
        Id = Guid.NewGuid().ToString();
        Name = "";
        Leagues = new List<League>();
    }
}