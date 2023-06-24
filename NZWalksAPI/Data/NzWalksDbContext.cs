using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Data;

public class NzWalksDbContext : DbContext
{
    public NzWalksDbContext(DbContextOptions dbContext) : base(dbContext)
    {
        
    }

    public DbSet<Region> Regions { get; set; }
    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<Walk> Walks { get; set; }
}