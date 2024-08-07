using AutoMapper;
using DAL.Storage;
using BLL.Interfaces;
using BLL.DTOs.LivingCreatures;
using Microsoft.EntityFrameworkCore;
using BLL.DTOs.LivingCreatures.Requests;
using DAL.Models.LivingCreatures;

namespace BLL.Services;

public class GenderService : IGenderService
{
    private readonly IMapper _mapper;
    private readonly GoTDBContext _dbContext;

    public GenderService(GoTDBContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ICollection<GenderDTO>> GetAllAsync()
    {
        var genders = await _dbContext
            .Genders
            .ToListAsync();

        var mappedGenders = _mapper.Map<List<GenderDTO>>(genders);
        return mappedGenders;
    }

    public async Task<GenderDTO> GetByIdAsync(Guid id)
    {
        var gender = await _dbContext
            .Genders
            .FirstOrDefaultAsync(x => x.Id == id);

        var mappedGender = _mapper.Map<GenderDTO>(gender);
        return mappedGender;
    }

    public async Task<GenderDTO> AddAsync(AddGenderDTO dto)
    {
        var gender = _mapper.Map<Gender>(dto);

        var addedGender = await _dbContext
            .Genders
            .AddAsync(gender);
        await _dbContext.SaveChangesAsync();

        var mappedGender = _mapper.Map<GenderDTO>(addedGender.Entity);
        return mappedGender;
    }

    public async Task<GenderDTO?> UpdateAsync(Guid id, UpdateGenderDTO dto)
    {
        var existingGender = await _dbContext
            .Genders
            .FirstOrDefaultAsync(x => x.Id == id);

        if (existingGender is null)
        {
            return null;
        }

        existingGender.Name = dto.Name;
        await _dbContext.SaveChangesAsync();

        var mappedGender = _mapper.Map<GenderDTO>(existingGender);
        return mappedGender;
    }

    public async Task<GenderDTO?> DeleteAsync(Guid id)
    {
        var existingGender = await _dbContext
            .Genders
            .FirstOrDefaultAsync(x => x.Id == id);

        if (existingGender is null)
        {
            return null;
        }

        _dbContext.Genders.Remove(existingGender);
        await _dbContext.SaveChangesAsync();

        var mappedGender = _mapper.Map<GenderDTO>(existingGender);
        return mappedGender;
    }
}