using BLL.DTOs.Locations;
using BLL.DTOs.Locations.Requests;

namespace BLL.Interfaces;

public interface ILocationService
{
    Task<ICollection<LocationDTO>> GetAllAsync();
    Task<LocationDTO?> GetByIdAsync(Guid id);
    Task<LocationDTO> AddAsync(AddLocationDTO dto);
    Task<LocationDTO?> UpdateAsync(Guid id, UpdateLocationDTO dto);
    Task<LocationDTO?> DeleteAsync(Guid id);
}
