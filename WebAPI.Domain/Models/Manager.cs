namespace WebAPI.Domain.Models ;

public class Manager
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string Nationality { get; set; }
    public string TeamId { get; set; }
    public Team Team { get; set; }

    public Manager()
    {
        Id = Guid.NewGuid().ToString();
        FullName = "";
        Nationality = "";
        TeamId = "";
        Team = new Team();
    }
}