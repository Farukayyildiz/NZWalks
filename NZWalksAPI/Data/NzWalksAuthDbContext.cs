using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalksAPI.Data;

public class NzWalksAuthDbContext :IdentityDbContext
{
    //Use brackets for seperate DbContexts
    public NzWalksAuthDbContext(DbContextOptions<NzWalksAuthDbContext> options) : base(options)
    {
    }
}