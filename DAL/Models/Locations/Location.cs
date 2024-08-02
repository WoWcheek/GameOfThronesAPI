using DAL.Models.Houses;

namespace DAL.Models.Locations;

public class Location
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Picture { get; set; }

    public Guid? LocationTypeId { get; set; }

    public virtual LocationType? LocationType { get; set; }

    public DateOnly? ExistedFrom { get; set; }

    public DateOnly? ExistedTo { get; set; }

    public virtual House? House { get; set; }
}
