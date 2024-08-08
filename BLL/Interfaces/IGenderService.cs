using BLL.DTOs.LivingCreatures;
using BLL.DTOs.LivingCreatures.Requests;

namespace BLL.Interfaces;

public interface IGenderService
{
    Task<ICollection<GenderDTO>> GetAllAsync();
    Task<GenderDTO?> GetByIdAsync(Guid id);
    Task<GenderDTO> AddAsync(AddGenderDTO dto);
    Task<GenderDTO?> UpdateAsync(Guid id, UpdateGenderDTO dto);
    Task<GenderDTO?> DeleteAsync(Guid id);
}
