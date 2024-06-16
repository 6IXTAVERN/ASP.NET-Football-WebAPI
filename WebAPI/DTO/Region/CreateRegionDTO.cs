using System.ComponentModel.DataAnnotations;

namespace WebAPI.Domain.DTO.Region;

public class CreateRegionDto
{
    [Required]
    public string Name { get; set; }
}