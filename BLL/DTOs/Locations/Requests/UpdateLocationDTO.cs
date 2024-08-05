namespace BLL.DTOs.Locations.Requests;

public class UpdateLocationDTO
{
    public string Name { get; set; } = string.Empty;

    public string? Picture { get; set; }

    public Guid? LocationTypeId { get; set; }

    public DateOnly? ExistedFrom { get; set; }

    public DateOnly? ExistedTo { get; set; }
}
