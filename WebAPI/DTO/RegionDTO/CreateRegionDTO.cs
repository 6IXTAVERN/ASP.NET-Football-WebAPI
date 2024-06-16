using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTO.RegionDTO;

public class CreateRegionDto
{
    [Required]
    [MaxLength(50, ErrorMessage = "Name cannot be over 50 characters")]
    public string Name { get; set; } = string.Empty;
}