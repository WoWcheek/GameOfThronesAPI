using BLL.DTOs.LivingCreatures;
using BLL.DTOs.LivingCreatures.Requests;

namespace BLL.Interfaces;

public interface IPetService
{
    Task<ICollection<PetDTO>> GetAllAsync();
    Task<PetDTO?> GetByIdAsync(Guid id);
    Task<PetDTO> AddAsync(AddPetDTO dto);
    Task<PetDTO?> UpdateAsync(Guid id, UpdatePetDTO dto);
    Task<PetDTO?> DeleteAsync(Guid id);
}
