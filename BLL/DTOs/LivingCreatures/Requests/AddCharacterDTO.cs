namespace BLL.DTOs.LivingCreatures.Requests;

public class AddCharacterDTO
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string? AlsoKnownAs { get; set; }

    public Guid GenderId { get; set; }

    public Guid HouseId { get; set; }

    public int? YearOfBirth { get; set; }

    public int? MonthOfBirth { get; set; }

    public int? DayOfBirth { get; set; }

    public string? Biography { get; set; }

    public string? Photo { get; set; }

    public List<Guid> PetsId { get; set; } = new List<Guid>();
}
