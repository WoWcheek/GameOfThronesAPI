using AutoMapper;
using DAL.Storage;
using BLL.Interfaces;
using BLL.DTOs.LivingCreatures;
using DAL.Models.LivingCreatures;
using Microsoft.EntityFrameworkCore;
using BLL.DTOs.LivingCreatures.Requests;

namespace BLL.Services;

public class PetTypeService : IPetTypeService
{
    private readonly IMapper _mapper;
    private readonly GoTDBContext _dbContext;

    public PetTypeService(GoTDBContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ICollection<PetTypeDTO>> GetAllAsync()
    {
        var petTypes = await _dbContext
            .PetTypes
            .ToListAsync();

        var mappedPetTypes = _mapper.Map<List<PetTypeDTO>>(petTypes);
        return mappedPetTypes;
    }

    public async Task<PetTypeDTO> GetByIdAsync(Guid id)
    {
        var petType = await _dbContext
            .PetTypes
            .FirstOrDefaultAsync(x => x.Id == id);

        var mappedPetType = _mapper.Map<PetTypeDTO>(petType);
        return mappedPetType;
    }

    public async Task<PetTypeDTO> AddAsync(AddPetTypeDTO dto)
    {
        var petType = _mapper.Map<PetType>(dto);

        var addedPetType = await _dbContext
            .PetTypes
            .AddAsync(petType);
        await _dbContext.SaveChangesAsync();

        var mappedPetType = _mapper.Map<PetTypeDTO>(addedPetType.Entity);
        return mappedPetType;
    }

    public async Task<PetTypeDTO?> UpdateAsync(Guid id, UpdatePetTypeDTO dto)
    {
        var existingPetType = await _dbContext
            .PetTypes
            .FirstOrDefaultAsync(x => x.Id == id);

        if (existingPetType is null)
        {
            return null;
        }

        existingPetType.Name = dto.Name;
        await _dbContext.SaveChangesAsync();

        var mappedPetType = _mapper.Map<PetTypeDTO>(existingPetType);
        return mappedPetType;
    }

    public async Task<PetTypeDTO?> DeleteAsync(Guid id)
    {
        var existingPetType = await _dbContext
            .PetTypes
            .FirstOrDefaultAsync(x => x.Id == id);

        if (existingPetType is null)
        {
            return null;
        }

        _dbContext.PetTypes.Remove(existingPetType);
        await _dbContext.SaveChangesAsync();

        var mappedPetType = _mapper.Map<PetTypeDTO>(existingPetType);
        return mappedPetType;
    }
}
