namespace DAL.Models.LivingCreatures;

public class Pet
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public Guid PetTypeId { get; set; }

    public virtual PetType PetType { get; set; } = null!;

    public Guid? GenderId { get; set; }

    public virtual Gender? Gender { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public virtual Character Owner { get; set; } = null!;
}
