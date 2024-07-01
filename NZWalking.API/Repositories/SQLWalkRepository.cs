using Microsoft.EntityFrameworkCore;
using NZWalking.API.Data;
using NZWalking.API.Models.Domain;

namespace NZWalking.API.Repositories
{
    public class SQLWalkRepository(NZWalksDbContext dbContext) : IWalkRepository
    {
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.AddAsync<Walk>(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync() => await dbContext.Walks.ToListAsync();
    }
}