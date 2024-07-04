using Microsoft.EntityFrameworkCore;
using NZWalking.API.Data;
using NZWalking.API.Models.Domain;
using NZWalking.API.Utils;

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

        public async Task<List<Walk>> GetAllAsync(string? filterFieldName = null, string? filterValue = null, string? filterComparisonOperator = null,
            string? sortBy = null, bool isAscending = true)
        {
            var walks = dbContext.Walks.Include(x => x.Difficulty).Include(x => x.Region).AsQueryable();
            //Filtering
            if (string.IsNullOrWhiteSpace(filterFieldName) is false && string.IsNullOrWhiteSpace(filterValue) is false)
            {
                if (filterFieldName.Equals(nameof(Walk.Name), StringComparison.OrdinalIgnoreCase))
                    walks = walks.Where(x => x.Name.Contains(filterValue));

                else if (string.IsNullOrWhiteSpace(filterComparisonOperator) is false)
                {
                    if (filterFieldName.Equals(nameof(Walk.LengthInKm), StringComparison.OrdinalIgnoreCase))
                    {
                        if (Enum.TryParse(filterComparisonOperator, true, out LogicOperator logicOperator))
                        {
                            //select concrete strategy based on LogicOperator
                            //walks = walks.Where(x => x.LengthInKm > double.Parse(filterValue));
                        }
                    }
                }
            }
            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy) is false)
            {
                if (sortBy.Equals(nameof(Walk.Name), StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals(nameof(Walk.LengthInKm), StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }
            return await walks.ToListAsync();
        }

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
            item.LengthInKm = walk.LengthInKm;
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