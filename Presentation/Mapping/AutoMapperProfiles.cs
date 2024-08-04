using AutoMapper;
using BLL.DTOs.Locations;
using BLL.DTOs.Locations.Requests;
using DAL.Models.Locations;

namespace Presentation.Mapping;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<LocationType, LocationTypeDTO>();
        CreateMap<AddLocationTypeDTO, LocationType>();
    }
}
