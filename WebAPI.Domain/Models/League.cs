using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebAPI.Domain.Models;

public class League
{
    public string Id { get; set; }
    public string Name { get; set; }
    public ICollection<Team> Teams { get; set; }
    public string RegionId { get; set; }
    public Region Region { get; set; }

    public League()
    {
        Id = Guid.NewGuid().ToString();
        Name = "";
        Teams = new List<Team>();
    }
}