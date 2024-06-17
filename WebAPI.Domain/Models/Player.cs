namespace WebAPI.Domain.Models;

public class Player
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string FullName { get; set; }
    public required string Nationality { get; set; }
    public required string Position { get; set; }
    public string? TeamId { get; set; }
    public Team? Team { get; set; }
}