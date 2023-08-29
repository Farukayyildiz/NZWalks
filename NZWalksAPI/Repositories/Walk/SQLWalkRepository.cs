using AutoMapper.Configuration;
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
}