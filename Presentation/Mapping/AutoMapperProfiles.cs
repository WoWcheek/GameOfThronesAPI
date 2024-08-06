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
        CreateMap<AddLocationDTO, Location>()
            .ForMember(x => x.ExistedFrom, opt =>
            {
                opt.PreCondition(x => x.ExistedFromYear != null && x.ExistedFromMonth != null && x.ExistedFromDay != null);
                opt.MapFrom(x => new DateOnly((int) x.ExistedFromYear!, (int)x.ExistedFromMonth!, (int)x.ExistedFromDay!));
            })
            .ForMember(x => x.ExistedTo, opt =>
            {
                opt.PreCondition(x => x.ExistedToYear != null && x.ExistedToMonth != null && x.ExistedToDay != null);
                opt.MapFrom(x => new DateOnly((int)x.ExistedToYear!, (int)x.ExistedToMonth!, (int)x.ExistedToDay!));
            });
    }
}
