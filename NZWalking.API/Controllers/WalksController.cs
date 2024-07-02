using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalking.API.CustomActionFilters;
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
        [ValidateModel]
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
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);
            if(walkDomainModel is null) return NotFound();
            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDTO updateWalkRequestDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDTO);
            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);
            if (walkDomainModel is null) return NotFound();
            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id) 
        {
            var deletedWalkDomainModel = await walkRepository.DeleteAsync(id);
            if (deletedWalkDomainModel is null) return BadRequest();
            return Ok(mapper.Map<WalkDTO>(deletedWalkDomainModel));
        }
    }
}
