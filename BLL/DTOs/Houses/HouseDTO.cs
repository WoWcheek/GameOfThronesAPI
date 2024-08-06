namespace BLL.DTOs.Houses;

public class HouseDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Motto { get; set; }

    public string? CrestPicture { get; set; }

    public HouseLocationDTO LocatedAt { get; set; } = null!;

    public List<HouseMemberDTO> Members { get; set; } = new List<HouseMemberDTO>();
}
