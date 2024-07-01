using Microsoft.EntityFrameworkCore;
using NZWalking.API.Models.Domain;

namespace NZWalking.API.Data
{
    public class NZWalksDbContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
    {
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}