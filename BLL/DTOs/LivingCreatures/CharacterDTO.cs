namespace BLL.DTOs.LivingCreatures;

public class CharacterDTO
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string? AlsoKnownAs { get; set; }

    public string Gender { get; set; } = string.Empty;

    public HouseOfCharacterDTO House { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string? Biography { get; set; }

    public string? Photo { get; set; }

    public List<PetOfCharacterDTO> Pets { get; set; } = new List<PetOfCharacterDTO>();
}
