using AutoMapper;
using DAL.Storage;
using BLL.Interfaces;
using BLL.DTOs.LivingCreatures;
using DAL.Models.LivingCreatures;
using Microsoft.EntityFrameworkCore;
using BLL.DTOs.LivingCreatures.Requests;
using BLL.Exceptions;

namespace BLL.Services;

public class CharacterSesrvice : ICharacterService
{
    private readonly IMapper _mapper;
    private readonly GoTDBContext _dbContext;

    public CharacterSesrvice(GoTDBContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ICollection<CharacterDTO>> GetAllAsync()
    {
        var characters = await _dbContext
            .Characters
            .Include(x => x.Gender)
            .Include(x => x.House)
            .ThenInclude(x => x.Location)
            .ThenInclude(x => x.LocationType)
            .Include(x => x.Pets)
            .ThenInclude(x => x.PetType)
            .Include(x => x.Pets)
            .ThenInclude(x => x.Gender)
            .ToListAsync();

        var mappedCharacters = _mapper.Map<List<CharacterDTO>>(characters);
        return mappedCharacters;
    }

    public async Task<CharacterDTO?> GetByIdAsync(Guid id)
    {
        var character = await _dbContext
            .Characters
            .Include(x => x.Gender)
            .Include(x => x.House)
            .ThenInclude(x => x.Location)
            .ThenInclude(x => x.LocationType)
            .Include(x => x.Pets)
            .ThenInclude(x => x.PetType)
            .Include(x => x.Pets)
            .ThenInclude(x => x.Gender)
            .FirstOrDefaultAsync(x => x.Id == id);

        var mappedCharacter = _mapper.Map<CharacterDTO>(character);
        return mappedCharacter;
    }

    public async Task<CharacterDTO> AddAsync(AddCharacterDTO dto)
    {
        var character = _mapper.Map<Character>(dto);

        var addedCharacter = (await _dbContext
            .Characters
            .AddAsync(character)).Entity;

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            if (ex.InnerException?.Message.Contains("GenderId") ?? false)
            {
                throw new ForeignKeyToNonExistentObjectException("Gender ID", "Gender");
            }
            if (ex.InnerException?.Message.Contains("HouseId") ?? false)
            {
                throw new ForeignKeyToNonExistentObjectException("House ID", "House");
            }
            if (ex.InnerException?.Message.Contains("PetId") ?? false)
            {
                throw new ForeignKeyToNonExistentObjectException("Pet ID", "Pet");
            }
            throw ex;
        }

        addedCharacter = await _dbContext
            .Characters
            .Include(x => x.Gender)
            .Include(x => x.House)
            .ThenInclude(x => x.Location)
            .ThenInclude(x => x.LocationType)
            .Include(x => x.Pets)
            .ThenInclude(x => x.PetType)
            .Include(x => x.Pets)
            .ThenInclude(x => x.Gender)
            .FirstOrDefaultAsync(x => x.Id == addedCharacter.Id);

        var mappedCharacter = _mapper.Map<CharacterDTO>(addedCharacter);
        return mappedCharacter;
    }

    public async Task<CharacterDTO?> UpdateAsync(Guid id, UpdateCharacterDTO dto)
    {
        var existingCharacter = await _dbContext
            .Characters
            .FirstOrDefaultAsync(x => x.Id == id);

        if (existingCharacter is null)
        {
            return null;
        }

        existingCharacter.FirstName = dto.FirstName;
        existingCharacter.LastName = dto.LastName;
        existingCharacter.AlsoKnownAs = dto.AlsoKnownAs;
        existingCharacter.GenderId = dto.GenderId;
        existingCharacter.HouseId = dto.HouseId;
        existingCharacter.Biography = dto.Biography;
        existingCharacter.Photo = dto.Photo;
        existingCharacter.PetsId = dto.PetsId;

        if (dto.YearOfBirth is not null &&
            dto.MonthOfBirth is not null &&
            dto.DayOfBirth is not null)
        {
            existingCharacter.DateOfBirth =
                new DateOnly((int)dto.YearOfBirth!, (int)dto.MonthOfBirth!, (int)dto.DayOfBirth!);
        }

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            if (ex.InnerException?.Message.Contains("GenderId") ?? false)
            {
                throw new ForeignKeyToNonExistentObjectException("Gender ID", "Gender");
            }
            if (ex.InnerException?.Message.Contains("HouseId") ?? false)
            {
                throw new ForeignKeyToNonExistentObjectException("House ID", "House");
            }
            if (ex.InnerException?.Message.Contains("PetId") ?? false)
            {
                throw new ForeignKeyToNonExistentObjectException("Pet ID", "Pet");
            }
            throw ex;
        }

        existingCharacter = await _dbContext
            .Characters
            .Include(x => x.Gender)
            .Include(x => x.House)
            .ThenInclude(x => x.Location)
            .ThenInclude(x => x.LocationType)
            .Include(x => x.Pets)
            .ThenInclude(x => x.PetType)
            .Include(x => x.Pets)
            .ThenInclude(x => x.Gender)
            .FirstOrDefaultAsync(x => x.Id == id);

        var mappedCharacter = _mapper.Map<CharacterDTO>(existingCharacter);
        return mappedCharacter;
    }

    public async Task<CharacterDTO?> DeleteAsync(Guid id)
    {
        var existingCharacter = await _dbContext
            .Characters
            .Include(x => x.Gender)
            .Include(x => x.House)
            .ThenInclude(x => x.Location)
            .ThenInclude(x => x.LocationType)
            .Include(x => x.Pets)
            .ThenInclude(x => x.PetType)
            .Include(x => x.Pets)
            .ThenInclude(x => x.Gender)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (existingCharacter is null)
        {
            return null;
        }

        _dbContext.Characters.Remove(existingCharacter);
        await _dbContext.SaveChangesAsync();

        var mappedCharacter = _mapper.Map<CharacterDTO>(existingCharacter);
        return mappedCharacter;
    }
}