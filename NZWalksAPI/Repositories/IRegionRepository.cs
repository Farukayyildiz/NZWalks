using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories;

public interface IRegionRepository
{
    Task<List<Region>> GetAllAsync();
    Task<Region?> GetByIdAsync(Guid Id);
    Task<Region> CreateAsync(Region region);
}