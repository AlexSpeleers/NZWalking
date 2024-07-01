using Microsoft.AspNetCore.Mvc;
using NZWalking.API.Controllers;
using NZWalking.API.Models.Domain;
using NZWalking.API.Models.DTO;

namespace NZWalking.API.Repositories
{
    public interface IRegionRepository
    {
        Task<Region> CreateAsync(Region region);
        Task<Region?> DeleteAsync(Guid id);
        Task<List<Region>> GetAllAsync();
        Task<Region?> GetByIdAsync(Guid id);
        Task<Region?> UpdateAsync(Guid id, Region region);
    }
}