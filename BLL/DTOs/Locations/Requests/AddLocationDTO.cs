namespace BLL.DTOs.Locations.Requests;

public class AddLocationDTO
{
    public string Name { get; set; } = string.Empty;

    public string? Picture { get; set; }

    public Guid? LocationTypeId { get; set; }

    public int? ExistedFromYear { get; set; }

    public int? ExistedFromMonth { get; set; }

    public int? ExistedFromDay { get; set; }

    public int? ExistedToYear { get; set; }

    public int? ExistedToMonth { get; set; }

    public int? ExistedToDay { get; set; }
}
