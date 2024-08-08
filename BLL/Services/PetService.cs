using AutoMapper;
using DAL.Storage;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.DTOs.LivingCreatures;
using DAL.Models.LivingCreatures;
using Microsoft.EntityFrameworkCore;
using BLL.DTOs.LivingCreatures.Requests;

namespace BLL.Services;

public class PetService : IPetService
{
    private readonly IMapper _mapper;
    private readonly GoTDBContext _dbContext;

    public PetService(GoTDBContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ICollection<PetDTO>> GetAllAsync()
    {
        var pets = await _dbContext
            .Pets
            .Include(x => x.Gender)
            .Include(x => x.PetType)
            .Include(x => x.Owner)
            .ThenInclude(x => x.House)
            .Include(x => x.Owner)
            .ThenInclude(x => x.Gender)
            .ToListAsync();

        var mappedPets = _mapper.Map<List<PetDTO>>(pets);
        return mappedPets;
    }

    public async Task<PetDTO?> GetByIdAsync(Guid id)
    {
        var pet = await _dbContext
            .Pets
            .Include(x => x.Gender)
            .Include(x => x.PetType)
            .Include(x => x.Owner)
            .ThenInclude(x => x.House)
            .Include(x => x.Owner)
            .ThenInclude(x => x.Gender)
            .FirstOrDefaultAsync(x => x.Id == id);

        var mappedPet = _mapper.Map<PetDTO>(pet);
        return mappedPet;
    }

    public async Task<PetDTO> AddAsync(AddPetDTO dto)
    {
        Pet pet;

        try
        {
            pet = _mapper.Map<Pet>(dto);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("un-representable DateTime"))
            {
                throw new InvalidDateException("Date of birth");
            }
            throw ex;
        }

        var addedPet = (await _dbContext
            .Pets
            .AddAsync(pet)).Entity;

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            if (ex.InnerException?.Message.Contains("PetTypeId") ?? false)
            {
                throw new ForeignKeyToNonExistentObjectException("Pet type ID", "Pet type");
            }
            if (ex.InnerException?.Message.Contains("GenderId") ?? false)
            {
                throw new ForeignKeyToNonExistentObjectException("Gender ID", "Gender");
            }
            throw ex;
        }

        addedPet = await _dbContext
            .Pets
            .Include(x => x.Gender)
            .Include(x => x.PetType)
            .Include(x => x.Owner)
            .ThenInclude(x => x.House)
            .Include(x => x.Owner)
            .ThenInclude(x => x.Gender)
            .FirstOrDefaultAsync(x => x.Id == addedPet.Id);

        var mappedPet = _mapper.Map<PetDTO>(addedPet);
        return mappedPet;
    }

    public async Task<PetDTO?> UpdateAsync(Guid id, UpdatePetDTO dto)
    {
        var existingPet = await _dbContext
            .Pets
            .FirstOrDefaultAsync(x => x.Id == id);

        if (existingPet is null)
        {
            return null;
        }

        existingPet.Name = dto.Name;
        existingPet.PetTypeId = dto.PetTypeId;
        existingPet.GenderId = dto.GenderId;

        if (dto.YearOfBirth is not null &&
            dto.MonthOfBirth is not null &&
            dto.DayOfBirth is not null)
        {
            try
            {
                existingPet.DateOfBirth =
                    new DateOnly((int)dto.YearOfBirth!, (int)dto.MonthOfBirth!, (int)dto.DayOfBirth!);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("un-representable DateTime"))
                {
                    throw new InvalidDateException("Date of birth");
                }
                throw ex;
            }
        }

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            if (ex.InnerException?.Message.Contains("PetTypeId") ?? false)
            {
                throw new ForeignKeyToNonExistentObjectException("Pet type ID", "Pet type");
            }
            if (ex.InnerException?.Message.Contains("GenderId") ?? false)
            {
                throw new ForeignKeyToNonExistentObjectException("Gender ID", "Gender");
            }
            throw ex;
        }

        existingPet = await _dbContext
            .Pets
            .Include(x => x.Gender)
            .Include(x => x.PetType)
            .Include(x => x.Owner)
            .ThenInclude(x => x.House)
            .Include(x => x.Owner)
            .ThenInclude(x => x.Gender)
            .FirstOrDefaultAsync(x => x.Id == id);

        var mappedPet = _mapper.Map<PetDTO>(existingPet);
        return mappedPet;
    }

    public async Task<PetDTO?> DeleteAsync(Guid id)
    {
        var existingPet = await _dbContext
            .Pets
            .Include(x => x.Gender)
            .Include(x => x.PetType)
            .Include(x => x.Owner)
            .ThenInclude(x => x.House)
            .Include(x => x.Owner)
            .ThenInclude(x => x.Gender)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (existingPet is null)
        {
            return null;
        }

        _dbContext.Pets.Remove(existingPet);
        await _dbContext.SaveChangesAsync();

        var mappedPet = _mapper.Map<PetDTO>(existingPet);
        return mappedPet;
    }
}