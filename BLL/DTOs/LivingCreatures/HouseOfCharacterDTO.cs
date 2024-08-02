using BLL.DTOs.Houses;

namespace BLL.DTOs.LivingCreatures;

internal class HouseOfCharacterDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Motto { get; set; }

    public string? CrestPicture { get; set; }

    public HouseLocationDTO LocatedAt { get; set; } = null!;
}
