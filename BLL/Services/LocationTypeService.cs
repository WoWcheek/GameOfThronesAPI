using AutoMapper;
using DAL.Storage;
using BLL.Interfaces;
using BLL.DTOs.Locations;
using DAL.Models.Locations;
using BLL.DTOs.Locations.Requests;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class LocationTypeService : ILocationTypeService
{
    private readonly IMapper _mapper;
    private readonly GoTDBContext _dbContext;

    public LocationTypeService(GoTDBContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ICollection<LocationTypeDTO>> GetAllAsync()
    {
        var locationTypes = await _dbContext
            .LocationTypes
            .ToListAsync();

        var mappedLocationTypes = _mapper.Map<List<LocationTypeDTO>>(locationTypes);
        return mappedLocationTypes;
    }

    public async Task<LocationTypeDTO> GetByIdAsync(Guid id)
    {
        var locationType = await _dbContext
            .LocationTypes
            .FirstOrDefaultAsync(x => x.Id == id);

        var mappedLocationType = _mapper.Map<LocationTypeDTO>(locationType);
        return mappedLocationType;
    }

    public async Task<LocationTypeDTO> AddAsync(AddLocationTypeDTO dto)
    {
        var locationType = _mapper.Map<LocationType>(dto);

        var addedLocationType = await _dbContext
            .LocationTypes
            .AddAsync(locationType);
        await _dbContext.SaveChangesAsync();

        var mappedLocationType = _mapper.Map<LocationTypeDTO>(addedLocationType.Entity);
        return mappedLocationType;
    }

    public async Task<LocationTypeDTO?> UpdateAsync(Guid id, UpdateLocationTypeDTO dto)
    {
        var existingLocationType = await _dbContext
            .LocationTypes
            .FirstOrDefaultAsync(x => x.Id == id);

        if (existingLocationType is null)
        {
            return null;
        }

        existingLocationType.Name = dto.Name;
        await _dbContext.SaveChangesAsync();

        var mappedLocationType = _mapper.Map<LocationTypeDTO>(existingLocationType);
        return mappedLocationType;
    }

    public async Task<LocationTypeDTO?> DeleteAsync(Guid id)
    {
        var existingLocationType = await _dbContext
            .LocationTypes
            .FirstOrDefaultAsync(x => x.Id == id);

        if (existingLocationType is null)
        {
            return null;
        }

        _dbContext.LocationTypes.Remove(existingLocationType);
        await _dbContext.SaveChangesAsync();

        var mappedLocationType = _mapper.Map<LocationTypeDTO>(existingLocationType);
        return mappedLocationType;
    }
}
