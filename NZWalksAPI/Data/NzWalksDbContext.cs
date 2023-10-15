using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Data;

public class NzWalksDbContext : DbContext
{
    public NzWalksDbContext(DbContextOptions<NzWalksDbContext> dbContext) : base(dbContext)
    {
    }

    public DbSet<Region> Regions { get; set; }
    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<Walk> Walks { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Seed the data for difficulties
        //Easy , Mediuam, Hard

        var difficulties = new List<Difficulty>()
        {
            new Difficulty()
            {
                Id = Guid.Parse("62677E9D-6DF8-4AA5-BE68-A77E14BD4149"),
                Name = "Easy"
            },
            new Difficulty()
            {
                Id = Guid.Parse("72C9A7AF-F0B5-44DD-9E77-550319D4E074"),
                Name = "Medium"
            },
            new Difficulty()
            {
                Id = Guid.Parse("E5A0D65E-5E17-41F3-B10E-4E25B9F89FD5"),
                Name = "Hard"
            }
        };

        //Seed difficulties to the database
        modelBuilder.Entity<Difficulty>().HasData(difficulties);


        //Seed data for Regions
        var regions = new List<Region>()
        {
            new Region()
            {
                Id = Guid.Parse("33523F5C-554B-4455-8EA4-C713B792CDB1"),
                Name = "Auckland",
                Code = "AKL",
                RegionImageUrl =
                    "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
            },
            new Region
            {
                Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                Name = "Northland",
                Code = "NTL",
                RegionImageUrl = null
            },
            new Region
            {
                Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                Name = "Bay Of Plenty",
                Code = "BOP",
                RegionImageUrl = null
            },
            new Region
            {
                Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                Name = "Wellington",
                Code = "WGN",
                RegionImageUrl =
                    "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
            },
            new Region
            {
                Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                Name = "Nelson",
                Code = "NSN",
                RegionImageUrl =
                    "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
            },
            new Region
            {
                Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                Name = "Southland",
                Code = "STL",
                RegionImageUrl = null
            },
        };

        modelBuilder.Entity<Region>().HasData(regions);
    }
}