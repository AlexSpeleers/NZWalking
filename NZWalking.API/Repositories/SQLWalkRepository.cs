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

        public async Task<List<Walk>> GetAllAsync() => await dbContext.Walks.Include(x => x.Difficulty).Include(x => x.Region).ToListAsync();

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks.
                Include(x => x.Difficulty).
                Include(x => x.Region).
                FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var item = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (item is null) return null;
            //item = walk;
            item.Name = walk.Name;
            item.Description = walk.Description;
            item.LenghtInKm = walk.LenghtInKm;
            item.WalkImageUrl = walk.WalkImageUrl;
            item.DifficultyId = walk.DifficultyId;
            item.RegionId = walk.RegionId;
            await dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk is null) return null;
            dbContext.Walks.Remove(existingWalk);
            await dbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}