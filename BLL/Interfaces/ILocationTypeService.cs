using BLL.DTOs.Locations;
using BLL.DTOs.Locations.Requests;

namespace BLL.Interfaces;

public interface ILocationTypeService
{
    Task<ICollection<LocationTypeDTO>> GetAllAsync();
    Task<LocationTypeDTO> GetByIdAsync(Guid id);
    Task<LocationTypeDTO> AddAsync(AddLocationTypeDTO dto);
    Task<LocationTypeDTO?> UpdateAsync(Guid id, UpdateLocationTypeDTO dto);
    Task<LocationTypeDTO?> DeleteAsync(Guid id);
}
