using DAL.Models.Houses;

namespace DAL.Models.LivingCreatures;

public class Character
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string? AlsoKnownAs { get; set; }

    public Guid GenderId { get; set; }

    public virtual Gender Gender { get; set; } = null!;

    public Guid HouseId { get; set; }

    public virtual House House { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string? Biography { get; set; } = null;

    public string? Photo { get; set; } = null;

    public virtual ICollection<Guid> PetsId { get; set; } = new List<Guid>();

    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();
}
