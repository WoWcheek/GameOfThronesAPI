using AutoMapper;
using DAL.Storage;
using BLL.Interfaces;
using BLL.DTOs.Locations;
using DAL.Models.Locations;
using BLL.DTOs.Locations.Requests;
using Microsoft.EntityFrameworkCore;
using BLL.Exceptions;

namespace BLL.Services;

public class LocationService : ILocationService
{
    private readonly IMapper _mapper;
    private readonly GoTDBContext _dbContext;

    public LocationService(GoTDBContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ICollection<LocationDTO>> GetAllAsync()
    {
        var locations = await _dbContext
            .Locations
            .Include(x => x.House)
            .Include(x => x.LocationType)
            .ToListAsync();

        var mappedLocations = _mapper.Map<List<LocationDTO>>(locations);
        return mappedLocations;
    }

    public async Task<LocationDTO?> GetByIdAsync(Guid id)
    {
        var location = await _dbContext
            .Locations
            .Include(x => x.House)
            .Include(x => x.LocationType)
            .FirstOrDefaultAsync(x => x.Id == id);

        var mappedLocation = _mapper.Map<LocationDTO>(location);
        return mappedLocation;
    }

    public async Task<LocationDTO> AddAsync(AddLocationDTO dto)
    {
        var location = _mapper.Map<Location>(dto);

        var addedLocation = (await _dbContext
            .Locations
            .AddAsync(location)).Entity;

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            if (ex.InnerException?.Message.Contains("LocationTypeId") ?? false)
            {
                throw new ForeignKeyToNonExistentObjectException("Location type ID", "Location type");
            }
            throw ex;
        }

        addedLocation = await _dbContext
            .Locations
            .Include(x => x.House)
            .Include(x => x.LocationType)
            .FirstOrDefaultAsync(x => x.Id == addedLocation.Id);

        var mappedLocation = _mapper.Map<LocationDTO>(addedLocation);
        return mappedLocation;
    }

    public async Task<LocationDTO?> UpdateAsync(Guid id, UpdateLocationDTO dto)
    {
        var existingLocation = await _dbContext
            .Locations
            .FirstOrDefaultAsync(x => x.Id == id);

        if (existingLocation is null)
        {
            return null;
        }

        existingLocation.Name = dto.Name;
        existingLocation.Picture = dto.Picture;
        existingLocation.LocationTypeId = dto.LocationTypeId;

        if (dto.ExistedFromYear is not null &&
            dto.ExistedFromMonth is not null &&
            dto.ExistedFromDay is not null)
        {
            existingLocation.ExistedFrom =
                new DateOnly((int)dto.ExistedFromYear!, (int)dto.ExistedFromMonth!, (int)dto.ExistedFromDay!);
        }

        if (dto.ExistedToYear is not null &&
            dto.ExistedToMonth is not null &&
            dto.ExistedToDay is not null)
        {
            existingLocation.ExistedFrom =
                new DateOnly((int)dto.ExistedToYear!, (int)dto.ExistedToMonth!, (int)dto.ExistedToDay!);
        }

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            if (ex.InnerException?.Message.Contains("LocationTypeId") ?? false)
            {
                throw new ForeignKeyToNonExistentObjectException("Location type ID", "Location type");
            }
            throw ex;
        }

        existingLocation = await _dbContext
            .Locations
            .Include(x => x.House)
            .Include(x => x.LocationType)
            .FirstOrDefaultAsync(x => x.Id == id);

        var mappedLocation = _mapper.Map<LocationDTO>(existingLocation);
        return mappedLocation;
    }

    public async Task<LocationDTO?> DeleteAsync(Guid id)
    {
        var existingLocation = await _dbContext
            .Locations
            .Include(x => x.House)
            .Include(x => x.LocationType)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (existingLocation is null)
        {
            return null;
        }

        _dbContext.Locations.Remove(existingLocation);
        await _dbContext.SaveChangesAsync();

        var mappedLocation = _mapper.Map<LocationDTO>(existingLocation);
        return mappedLocation;
    }
}
