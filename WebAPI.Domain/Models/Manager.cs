namespace WebAPI.Domain.Models ;

public class Manager
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string FullName { get; set; } 
    public required string Nationality { get; set; }
    public string? TeamId { get; set; }
    public Team? Team { get; set; }
}