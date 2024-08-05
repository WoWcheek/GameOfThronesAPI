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

        CreateMap<Location, LocationDTO>()
            .ForMember(x => x.Type, opt => opt.MapFrom(x => x.LocationType == null ? null : x.LocationType.Name))
            .ForMember(x => x.SeatOf, opt => opt.MapFrom(x => x.House == null ? null : new GovernmentDTO
            {
                Id = x.House.Id,
                Name = x.House.Name,
                CrestPicture = x.House.CrestPicture
            }));
        CreateMap<AddLocationDTO, Location>();
    }
}
