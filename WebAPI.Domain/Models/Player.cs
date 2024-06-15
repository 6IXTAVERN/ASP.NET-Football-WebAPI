using System.Text.Json.Serialization;

namespace WebAPI.Domain.Models ;

public class Player
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string Nationality { get; set; }
    public string Position { get; set; }
    public string? TeamId { get; set; }
    public Team? Team { get; set; }

    public Player()
    {
        Id = Guid.NewGuid().ToString();
        FullName = "";
        Nationality = "";
        Position = "";
        TeamId = null;
        Team = null;
    }
}