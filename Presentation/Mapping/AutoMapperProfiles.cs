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

        CreateMap<PetType, PetTypeDTO>();
        CreateMap<AddPetTypeDTO, PetType>();

        CreateMap<Pet, PetDTO>()
            .ForMember(x => x.Type, opt => opt.MapFrom(x => x.PetType.Name))
            .ForMember(x => x.Gender, opt => opt.MapFrom(x => x.Gender == null ? null :x.Gender.Name))
            .ForMember(x => x.Owner, opt => opt.MapFrom(x => x.Gender == null ? null : new PetOwnerDTO
            {
                Id = x.Owner!.Id,
                FirstName = x.Owner!.FirstName,
                LastName = x.Owner!.LastName,
                AlsoKnownAs = x.Owner!.AlsoKnownAs,
                Gender = x.Owner!.Gender.Name,
                HouseName = x.Owner.House.Name,
                Photo = x.Owner.Photo
            }));
        CreateMap<AddPetDTO, Pet>()
            .ForMember(x => x.DateOfBirth, opt =>
            {
                opt.PreCondition(x => x.YearOfBirth != null && x.MonthOfBirth != null && x.DayOfBirth != null);
                opt.MapFrom(x => new DateOnly((int)x.YearOfBirth!, (int)x.MonthOfBirth!, (int)x.DayOfBirth!));
            });

        CreateMap<Character, CharacterDTO>()
            .ForMember(x => x.Gender, opt => opt.MapFrom(x => x.Gender.Name))
            .ForMember(x => x.House, opt => opt.MapFrom(x => new HouseOfCharacterDTO
            {
                Id = x.House.Id,
                Name = x.House.Name,
                Motto = x.House.Motto,
                CrestPicture = x.House.CrestPicture,
                LocatedAt = new HouseLocationDTO
                {
                    Id = x.House.Location.Id,
                    Name = x.House.Location.Name,
                    Picture = x.House.Location.Picture
                }
            }))
            .ForMember(x => x.Pets, opt => opt.MapFrom(x => x.Pets.Select(y => new PetOfCharacterDTO
            {
                Id = y.Id,
                Name = y.Name,
                Type = y.PetType.Name,
                Gender = y.Gender == null ? null : y.Gender.Name
            })));
        CreateMap<AddCharacterDTO, Character>()
            .ForMember(x => x.DateOfBirth, opt =>
            {
                opt.PreCondition(x => x.YearOfBirth != null && x.MonthOfBirth != null && x.DayOfBirth != null);
                opt.MapFrom(x => new DateOnly((int)x.YearOfBirth!, (int)x.MonthOfBirth!, (int)x.DayOfBirth!));
            });
    }
}
