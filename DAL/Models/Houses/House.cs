using DAL.Models.Locations;
using DAL.Models.LivingCreatures;

namespace DAL.Models.Houses;

public class House
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Motto { get; set; }

    public string? CrestPicture { get; set; }

    public Guid LocationId { get; set; }

    public virtual Location Location { get; set; } = null!;

    public virtual ICollection<Character> Characters { get; set; } = new List<Character>();
}
