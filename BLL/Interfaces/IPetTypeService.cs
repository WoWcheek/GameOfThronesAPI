using BLL.DTOs.LivingCreatures;
using BLL.DTOs.LivingCreatures.Requests;

namespace BLL.Interfaces;

public interface IPetTypeService
{
    Task<ICollection<PetTypeDTO>> GetAllAsync();
    Task<PetTypeDTO> GetByIdAsync(Guid id);
    Task<PetTypeDTO> AddAsync(AddPetTypeDTO dto);
    Task<PetTypeDTO?> UpdateAsync(Guid id, UpdatePetTypeDTO dto);
    Task<PetTypeDTO?> DeleteAsync(Guid id);
}
