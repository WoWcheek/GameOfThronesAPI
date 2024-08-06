namespace DAL.Models.LivingCreatures;

public class Gender
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<Pet> Pets { get; set; } = new List<Pet>();

    public List<Character> Characters { get; set; } = new List<Character>();
}
