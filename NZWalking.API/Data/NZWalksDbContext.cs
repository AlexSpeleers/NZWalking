using Microsoft.EntityFrameworkCore;
using NZWalking.API.Models.Domain;

namespace NZWalking.API.Data
{
    public class NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : DbContext(dbContextOptions)
    {
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var difficulties = new List<Difficulty>()
            {
                new()
                {
                    Id=Guid.Parse("d8c7ca81-0223-4d78-8d11-8fa75516b39e"),
                    Name="Easy"
                },
                new()
                {
                    Id=Guid.Parse("2206d382-1fcb-4207-a783-c276c705b2bb"),
                    Name="Medium"
                },
                new()
                {
                    Id=Guid.Parse("6b64a666-70b1-4e3c-a7c4-0964d17e2aca"),
                    Name="Hard"
                }
            };
            modelBuilder.Entity<Difficulty>().HasData(difficulties);
            var regions = new List<Region>()
            {
                new()
                {
                    Id=Guid.Parse("4ccff635-ad84-4afe-b91f-6ca535f66982"),
                    Name="Auckland",
                    Code="AKL",
                    RegionImageUrl="https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new()
                {
                    Id=Guid.Parse("f05aa81b-8392-487a-bff5-21a78f29d818"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageUrl = null
                },
                new()
                {
                    Id=Guid.Parse("8da3828d-110a-4cd1-a0d2-79ba80a29a63"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionImageUrl = null
                },
                new()
                {
                    Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new()
                {
                    Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new()
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
}