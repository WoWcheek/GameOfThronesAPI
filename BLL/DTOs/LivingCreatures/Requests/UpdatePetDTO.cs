namespace BLL.DTOs.LivingCreatures.Requests;

public class UpdatePetDTO
{
    public string Name { get; set; } = string.Empty;

    public Guid PetTypeId { get; set; }

    public Guid? GenderId { get; set; }

    public int? YearOfBirth { get; set; }

    public int? MonthOfBirth { get; set; }

    public int? DayOfBirth { get; set; }

    public Guid? OwnerId { get; set; }
}
