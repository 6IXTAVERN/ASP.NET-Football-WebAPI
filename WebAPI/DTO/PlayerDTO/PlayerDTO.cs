using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Domain.Models;

namespace WebAPI.DTO.PlayerDTO;

public class PlayerDto
{
    public required string Id { get; set; } 
    public required string FullName { get; set; }
    public required string Nationality { get; set; } 

    [ForeignKey("TeamId")]
    public string? TeamId { get; set; }

    public Team? Team { get; set; }
    public required string Position { get; set; }
}