namespace BLL.DTOs.Houses.Requests;

public class UpdateHouseDTO
{
    public string Name { get; set; } = string.Empty;

    public string? Motto { get; set; }

    public string? CrestPicture { get; set; }

    public Guid LocationId { get; set; }
}
