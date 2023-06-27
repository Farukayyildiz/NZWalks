using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories;

public class SQLRegionRepository : IRegionRepository
{
    private readonly NzWalksDbContext _context;
    public SQLRegionRepository(NzWalksDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Region>> GetAllAsync()
    {
        return await _context.Regions.ToListAsync();
    }

    public async Task<Region?> GetByIdAsync(Guid Id)
    {
        return await _context.Regions.FirstOrDefaultAsync(x => x.Id == Id);
    }

    public async Task<Region> CreateAsync(Region region)
    {
        await _context.Regions.AddAsync(region);
        await _context.SaveChangesAsync();
        return region;
    }
}