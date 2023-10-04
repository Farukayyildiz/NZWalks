using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories;

public interface IWalkRepository
{
    Task<Walk> CreateAsync(Walk walk);
    Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null);
    Task<Walk?> GetWalkByIdAsync(Guid Id);
    Task<Walk?> UpdateAsync(Guid Id, Walk walk);
    Task<Walk?> DeleteAsync(Guid Id);
}