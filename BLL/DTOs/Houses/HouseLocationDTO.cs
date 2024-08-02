namespace BLL.DTOs.Houses;

internal class HouseLocationDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Picture { get; set; }
}
