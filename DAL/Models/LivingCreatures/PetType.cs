namespace DAL.Models.LivingCreatures;

public class PetType
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();
}
