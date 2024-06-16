using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Domain.Models;

namespace WebAPI.DTO.PlayerDTO;

public class PlayerDto
{
    public string Id { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;

    [ForeignKey("TeamId")]
    public string? TeamId { get; set; }

    public Team? Team { get; set; }
    public string Position { get; set; } = string.Empty;
}