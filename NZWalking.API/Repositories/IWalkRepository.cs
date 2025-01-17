﻿using NZWalking.API.Models.Domain;

namespace NZWalking.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(string? filterFieldName = null, string? filterValue = null,
             string? filterComparisonOperator = null, string? sortBy = null, bool isAscending = true,
             int pageNumber = 1, int pageSize = 1000);
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk?> UpdateAsync(Guid id, Walk walk);
        Task<Walk?> DeleteAsync(Guid id);
    }
}
