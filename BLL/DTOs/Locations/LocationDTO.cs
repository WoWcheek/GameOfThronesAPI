namespace BLL.DTOs.Locations;

internal class LocationDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Picture { get; set; }

    public string? Type { get; set; }

    public DateOnly? ExistedFrom { get; set; }

    public DateOnly? ExistedTo { get; set; }

    public virtual GovernmentDTO? SeatOf { get; set; }
}
