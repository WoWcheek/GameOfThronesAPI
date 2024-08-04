namespace BLL.DTOs.LivingCreatures;

public class PetOwnerDTO
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string? AlsoKnownAs { get; set; }

    public string Gender { get; set; } = string.Empty;

    public string HouseName { get; set; } = string.Empty;

    public string? Photo { get; set; } = null;
}
