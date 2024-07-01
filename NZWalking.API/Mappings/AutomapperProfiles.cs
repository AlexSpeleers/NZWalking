using AutoMapper;
using NZWalking.API.Models.Domain;
using NZWalking.API.Models.DTO;

namespace NZWalking.API.Mappings
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
        }
    }
}
