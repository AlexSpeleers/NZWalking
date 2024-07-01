using AutoMapper;
using NZWalking.API.Controllers;
using NZWalking.API.Models.Domain;
using NZWalking.API.Models.DTO;

namespace NZWalking.API.Mappings
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<AddRegionRequestDTO, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDTO, Region>().ReverseMap();
        }
    }
}