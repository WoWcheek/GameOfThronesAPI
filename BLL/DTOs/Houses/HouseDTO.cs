namespace BLL.DTOs.Houses;

internal class HouseDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Motto { get; set; }

    public string? CrestPicture { get; set; }

    public HouseLocationDTO LocatedAt { get; set; } = null!;

    public virtual ICollection<HouseMemberDTO> Members { get; set; } = new List<HouseMemberDTO>();
}
