using System.ComponentModel.DataAnnotations;

namespace WebAPI.Domain.DTO.Region;

public class UpdateRegionDto
{
    [Required]
    public string Name { get; set; }
}