using AutoMapper;
using BLL.DTOs.Houses;
using DAL.Models.Houses;
using BLL.DTOs.Locations;
using DAL.Models.Locations;
using BLL.DTOs.LivingCreatures;
using BLL.DTOs.Houses.Requests;
using DAL.Models.LivingCreatures;
using BLL.DTOs.Locations.Requests;
using BLL.DTOs.LivingCreatures.Requests;

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
                opt.MapFrom(x => new DateOnly((int)x.ExistedFromYear!, (int)x.ExistedFromMonth!, (int)x.ExistedFromDay!));
            })
            .ForMember(x => x.ExistedTo, opt =>
            {
                opt.PreCondition(x => x.ExistedToYear != null && x.ExistedToMonth != null && x.ExistedToDay != null);
                opt.MapFrom(x => new DateOnly((int)x.ExistedToYear!, (int)x.ExistedToMonth!, (int)x.ExistedToDay!));
            });

        CreateMap<House, HouseDTO>()
            .ForMember(x => x.LocatedAt, opt => opt.MapFrom(x =>
                new HouseLocationDTO
                {
                    Id = x.Location.Id,
                    Name = x.Location.Name,
                    Picture = x.Location.Picture
                }))
            .ForMember(x => x.Members, opt => opt.MapFrom(x => 
                x.Characters.Select(character => new HouseMemberDTO
                {
                    Id = character.Id,
                    FirstName = character.FirstName,
                    LastName = character.LastName,
                    AlsoKnownAs = character.AlsoKnownAs,
                    Gender = character.Gender.Name,
                    Photo = character.Photo
                })));
        CreateMap<AddHouseDTO, House>();

        CreateMap<Gender, GenderDTO>();
        CreateMap<AddGenderDTO, Gender>();
    }
}
