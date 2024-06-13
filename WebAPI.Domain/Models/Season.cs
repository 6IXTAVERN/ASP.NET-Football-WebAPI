namespace WebAPI.Domain.Models ;

public class Season
{
    public string Id { get; set; }
    public string Date { get; set; }
    public ICollection<Region> Regions { get; set; }

    public Season()
    {
        Id = Guid.NewGuid().ToString();
        Date = "2000/2001";
        Regions = new List<Region>();
    }
}