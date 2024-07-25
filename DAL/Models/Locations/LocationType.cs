namespace DAL.Models.Locations;

public class LocationType
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
}
