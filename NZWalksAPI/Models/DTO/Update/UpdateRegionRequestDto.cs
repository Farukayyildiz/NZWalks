using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO;

public class UpdateRegionRequestDto
{
    [Required]
    [MinLength(3, ErrorMessage = "Code has to be a minimum of 3 chrachters")]
    [MaxLength(3, ErrorMessage = "Code has to be a maximum of 3 chrachters")]
    public string Code { get; set; }

    [Required]
    [MaxLength(100, ErrorMessage = "Name has to be maximum 100 charachter")]
    public string Name { get; set; }

    public string? RegionImageUrl { get; set; }
}