namespace BLL.DTOs.Houses;

public class HouseMemberDTO
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string? AlsoKnownAs { get; set; }

    public string Gender { get; set; } = string.Empty;

    public string? Photo { get; set; } = null;
}
