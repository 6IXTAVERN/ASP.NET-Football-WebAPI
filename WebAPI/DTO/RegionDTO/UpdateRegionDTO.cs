using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTO.RegionDTO;

public class UpdateRegionDto
{
    [Required]
    [MaxLength(50, ErrorMessage = "Name cannot be over 50 characters")]
    public string Name { get; set; } = string.Empty;
}