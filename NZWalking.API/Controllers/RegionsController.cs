﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalking.API.Data;
using NZWalking.API.Models.Domain;
using NZWalking.API.Models.DTO;
using NZWalking.API.Repositories;

namespace NZWalking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var regionsDomain = await regionRepository.GetAllAsync();
            //var regions = new List<Region>
            //{
            //  new()
            //  {
            //        Id = Guid.NewGuid(),
            //        Name = "Wellington Region",
            //        Code = "WLG",
            //        RegionImageUrl = "https://www.americanexpress.com/de-de/amexcited/media/cache/default/cms/2023/06/wellington-neuseeland-titel.jpg"
            //  }
            //};
            //var regionsDTO = new List<RegionDTO>();
            //foreach (var regionDomain in regionsDomain)
            //    regionsDTO.Add(ConvertRegionToDTO(regionDomain));
            return Ok(mapper.Map<List<RegionDTO>>(regionsDomain));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.GetByIdAsync(id);
            //regionDomain = dbContext.Regions.FirstOrDefault(value => value.Id == id);
            if (regionDomain is null)
                return NotFound();
            var regionDTO = mapper.Map<RegionDTO>(regionDomain);
            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDTO.Code,
                Name = addRegionRequestDTO.Name,
                RegionImageUrl = addRegionRequestDTO.RegionImageUrl
            };

            await regionRepository.CreateAsync(regionDomainModel);
            var regionDTO = ConvertRegionToDTO(regionDomainModel);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = regionDomainModel.Id }, regionDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            var regionDomainModel = ConvertUpdateDTOtoRegion(updateRegionRequestDTO);
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
            if (regionDomainModel is null) return NotFound();

            var regionDTO = ConvertRegionToDTO(regionDomainModel);
            return Ok(regionDTO);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel is null) return NotFound();
            var regionDTO = ConvertRegionToDTO(regionDomainModel);
            return Ok(regionDTO);
        }

        #region helpers
        private RegionDTO ConvertRegionToDTO(Region source)
        {
            return new()
            {
                Id = source.Id,
                Code = source.Code,
                Name = source.Name,
                RegionImageUrl = source.RegionImageUrl,
            };
        }
        private Region ConvertUpdateDTOtoRegion(UpdateRegionRequestDTO updateRegionRequestDTO) 
        {
            return new Region()
            {
                Code = updateRegionRequestDTO.Code,
                Name = updateRegionRequestDTO.Name,
                RegionImageUrl = updateRegionRequestDTO.RegionImageUrl,
            };
        }
        #endregion
    }
}