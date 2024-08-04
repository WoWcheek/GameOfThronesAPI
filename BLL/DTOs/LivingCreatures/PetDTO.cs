namespace BLL.DTOs.LivingCreatures;

public class PetDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public string? Gender { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public PetOwnerDTO Owner { get; set; } = null!;
}
