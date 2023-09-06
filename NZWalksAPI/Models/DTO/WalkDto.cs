using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Models.DTO;

public class WalkDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double LengthInKm { get; set; }
    public string? WalkImageUrl { get; set; }

    //SQL Walk Repo dan gelecek Incluede metodu sayesinde id lere gerek kalmadan direkt olarak iki modelin DTO' suna gerekli bilgiler doldurulacak.
    public RegionDto Region { get; set; }
    public DifficultyDto Difficulty { get; set; }
}