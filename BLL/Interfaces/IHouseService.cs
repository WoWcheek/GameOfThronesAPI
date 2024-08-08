using BLL.DTOs.Houses;
using BLL.DTOs.Houses.Requests;

namespace BLL.Interfaces;

public interface IHouseService
{
    Task<ICollection<HouseDTO>> GetAllAsync();
    Task<HouseDTO?> GetByIdAsync(Guid id);
    Task<HouseDTO> AddAsync(AddHouseDTO dto);
    Task<HouseDTO?> UpdateAsync(Guid id, UpdateHouseDTO dto);
    Task<HouseDTO?> DeleteAsync(Guid id);
}
