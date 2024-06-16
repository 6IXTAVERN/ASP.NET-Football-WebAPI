using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Domain.Models;

namespace WebAPI.DTO.PlayerDTO;

public class PlayerDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int Nationality { get; set; }

    [ForeignKey("TeamId")]
    public int? TeamId { get; set; }

    public Team? Team { get; set; }
    public string Position { get; set; } = string.Empty;
}