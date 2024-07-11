﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalking.API.CustomActionFilters;
using NZWalking.API.Data;
using NZWalking.API.Models.Domain;
using NZWalking.API.Models.DTO;
using NZWalking.API.Repositories;
using System.Text.Json;

namespace NZWalking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger) : ControllerBase
    {
        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAllAsync()
        {
            logger.LogInformation("GetAllRegions action method was invoked.");

            var regionsDomain = await regionRepository.GetAllAsync();

            logger.LogInformation($"Finish GetAllRegions request with data:{JsonSerializer.Serialize(regionsDomain)}");
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
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.GetByIdAsync(id);
            //regionDomain = dbContext.Regions.FirstOrDefault(value => value.Id == id);
            if (regionDomain is null)
                return NotFound();
            return Ok(mapper.Map<RegionDTO>(regionDomain));
        }

        [HttpPost]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateAsync([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDTO);
            await regionRepository.CreateAsync(regionDomainModel);
            var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDTO);
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
            if (regionDomainModel is null) return NotFound();
            return Ok(mapper.Map<RegionDTO>(regionDomainModel));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel is null) return NotFound();
            return Ok(mapper.Map<RegionDTO>(regionDomainModel));
        }
    }
}