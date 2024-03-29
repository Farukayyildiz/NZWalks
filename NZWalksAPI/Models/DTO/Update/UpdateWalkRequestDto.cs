using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO;

public class UpdateWalkRequestDto
{
    [Required]
    [MaxLength(100,ErrorMessage = "Walk")]
    public string Name { get; set; }
    
    [Required]
    [MaxLength(1000,ErrorMessage = "Walk")]
    public string Description { get; set; }
    
    [Required]
    [Range(0,50)]
    public double LengthInKm { get; set; }
    
    [Required]
    public string? WalkImageUrl { get; set; }
    
    [Required]
    public Guid DifficultyId { get; set; }
    
    [Required]
    public Guid RegionId { get; set; }
}