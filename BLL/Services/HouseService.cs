using AutoMapper;
using DAL.Storage;
using BLL.Interfaces;
using BLL.DTOs.Houses;
using DAL.Models.Houses;
using BLL.DTOs.Houses.Requests;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class HouseService : IHouseService
{
    private readonly IMapper _mapper;
    private readonly GoTDBContext _dbContext;

    public HouseService(GoTDBContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ICollection<HouseDTO>> GetAllAsync()
    {
        var houses = await _dbContext
            .Houses
            .Include(x => x.Location)
            .Include(x => x.Characters)
            .ToListAsync();

        var mappedHouses = _mapper.Map<List<HouseDTO>>(houses);
        return mappedHouses;
    }

    public async Task<HouseDTO> GetByIdAsync(Guid id)
    {
        var house = await _dbContext
            .Houses
            .Include(x => x.Location)
            .Include(x => x.Characters)
            .FirstOrDefaultAsync(x => x.Id == id);

        var mappedHouse = _mapper.Map<HouseDTO>(house);
        return mappedHouse;
    }

    public async Task<HouseDTO> AddAsync(AddHouseDTO dto)
    {
        var house = _mapper.Map<House>(dto);

        var addedHouse = (await _dbContext
            .Houses
            .AddAsync(house)).Entity;

        await _dbContext.SaveChangesAsync();

        addedHouse = await _dbContext
            .Houses
            .Include(x => x.Location)
            .Include(x => x.Characters)
            .FirstOrDefaultAsync(x => x.Id == addedHouse.Id);

        var mappedHouse = _mapper.Map<HouseDTO>(addedHouse);
        return mappedHouse;
    }

    public async Task<HouseDTO?> UpdateAsync(Guid id, UpdateHouseDTO dto)
    {
        var existingHouse = await _dbContext
            .Houses
            .FirstOrDefaultAsync(x => x.Id == id);

        if (existingHouse is null)
        {
            return null;
        }

        existingHouse.Name = dto.Name;
        existingHouse.Motto = dto.Motto;
        existingHouse.CrestPicture = dto.CrestPicture;
        existingHouse.LocationId = dto.LocationId;

        await _dbContext.SaveChangesAsync();

        existingHouse = await _dbContext
            .Houses
            .Include(x => x.Location)
            .Include(x => x.Characters)
            .FirstOrDefaultAsync(x => x.Id == id);

        var mappedHouse = _mapper.Map<HouseDTO>(existingHouse);
        return mappedHouse;
    }

    public async Task<HouseDTO?> DeleteAsync(Guid id)
    {
        var existingHouse = await _dbContext
            .Houses
            .Include(x => x.Location)
            .Include(x => x.Characters)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (existingHouse is null)
        {
            return null;
        }

        _dbContext.Houses.Remove(existingHouse);
        await _dbContext.SaveChangesAsync();

        var mappedHouse = _mapper.Map<HouseDTO>(existingHouse);
        return mappedHouse;
    }
}