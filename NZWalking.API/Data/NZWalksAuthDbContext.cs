using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalking.API.Data
{
    public class NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : IdentityDbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerId = "ac82d10e-7619-418f-8f06-71c2e8134c95";
            var writerId = "9e724fac-13ee-4a78-b6b4-664ee15d4efe";

            var roles = new List<IdentityRole>
            {
                new()
                {
                    Id= readerId,
                    ConcurrencyStamp=readerId,
                    Name = "Reader",
                    NormalizedName="Reader".ToUpper()
                },
                new ()
                {
                    Id= writerId,
                    ConcurrencyStamp=writerId,
                    Name = "Writer",
                    NormalizedName="Writer".ToUpper()
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}