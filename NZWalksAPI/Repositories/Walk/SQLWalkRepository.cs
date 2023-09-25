using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories;

public class SQLWalkRepository : IWalkRepository
{
    private readonly NzWalksDbContext _context;

    public SQLWalkRepository(NzWalksDbContext context)
    {
        _context = context;
    }

    public async Task<Walk> CreateAsync(Walk walk)
    {
        await _context.Walks.AddAsync(walk);
        await _context.SaveChangesAsync();
        return walk;
    }


    public async Task<List<Walk>> GetAllAsync()
    {
        //Include ==> bring the related tables data according to their Id
        return await _context.Walks.Include("Difficulty").Include("Region").ToListAsync();
    }

    public async Task<Walk?> GetWalkByIdAsync(Guid Id)
    {
        return await _context.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == Id);
    }

    public async Task<Walk?> UpdateAsync(Guid Id, Walk walk)
    {
        var existingWalk = await _context.Walks.FirstOrDefaultAsync(x => x.Id == Id);

        if (existingWalk == null)
            return null;

        existingWalk.Name = walk.Name;
        existingWalk.LengthInKm = walk.LengthInKm;
        existingWalk.Description = walk.Description;
        existingWalk.WalkImageUrl = walk.WalkImageUrl;
        existingWalk.RegionId = walk.RegionId;
        existingWalk.DifficultyId = walk.DifficultyId;

        await _context.SaveChangesAsync();
        return existingWalk;
    }

    public async Task<Walk?> DeleteAsync(Guid Id)
    {
        var existingWalk = await _context.Walks.FirstOrDefaultAsync(x => x.Id == Id);

        if (existingWalk == null)
            return null;

        _context.Walks.Remove(existingWalk);
        await _context.SaveChangesAsync();
        return existingWalk;
    }
}