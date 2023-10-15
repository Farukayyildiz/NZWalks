using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalksAPI.Data;

public class NzWalksAuthDbContext : IdentityDbContext
{
    //Use brackets for seperate DbContexts
    public NzWalksAuthDbContext(DbContextOptions<NzWalksAuthDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        var readerId = "6C6EE19C-3D1D-4A21-B83B-71E9C3B2609A";
        var writerId = "B4FE8F9C-E253-4884-A5F2-EE6794C26118";

        var roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Id = readerId,
                ConcurrencyStamp = readerId,
                Name = "Reader",
                NormalizedName = "Reader".ToUpper()
            },
            
            new IdentityRole
            {
                Id = writerId,
                ConcurrencyStamp = writerId,
                Name = "OmerFaruk",
                NormalizedName = "OmerFaruk".ToUpper()
            }
        };

        //If there is no data in DB this line will add DB the neccesary fields. 
        builder.Entity<IdentityRole>().HasData(roles);  
    }
}