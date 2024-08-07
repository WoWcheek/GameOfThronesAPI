using BLL.DTOs.LivingCreatures;
using BLL.DTOs.LivingCreatures.Requests;

namespace BLL.Interfaces;

public interface ICharacterService
{
    Task<ICollection<CharacterDTO>> GetAllAsync();
    Task<CharacterDTO> GetByIdAsync(Guid id);
    Task<CharacterDTO> AddAsync(AddCharacterDTO dto);
    Task<CharacterDTO?> UpdateAsync(Guid id, UpdateCharacterDTO dto);
    Task<CharacterDTO?> DeleteAsync(Guid id);
}
