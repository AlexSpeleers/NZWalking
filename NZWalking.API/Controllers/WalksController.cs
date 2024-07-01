using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalking.API.Models.Domain;
using NZWalking.API.Models.DTO;
using NZWalking.API.Repositories;

namespace NZWalking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController(IMapper mapper, IWalkRepository walkRepository) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO) 
        {
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDTO);
            await walkRepository.CreateAsync(walkDomainModel);
            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var walksDomainModel = await walkRepository.GetAllAsync();
            return Ok(mapper.Map<List<WalkDTO>>(walksDomainModel));
        }
    }
}
