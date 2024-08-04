using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BLL.DTOs.Locations.Requests;

namespace Presentation.Controllers.Locations;

[Route("/api/[controller]")]
[ApiController]
public class LocationTypesController : ControllerBase
{
    private readonly ILocationTypeService _locationTypeService;

    public LocationTypesController(ILocationTypeService locationTypeService)
    {
        _locationTypeService = locationTypeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var locationTypes = await _locationTypeService.GetAllAsync();

        return Ok(locationTypes);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var locationType = await _locationTypeService.GetByIdAsync(id);

        if (locationType is null)
        {
            return NotFound();
        }

        return Ok(locationType);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddLocationTypeDTO request)
    {
        var addedLocationType = await _locationTypeService.AddAsync(request);

        return CreatedAtAction(nameof(GetById), new { id = addedLocationType.Id }, addedLocationType);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateLocationTypeDTO request)
    {
        var updatedLocationType = await _locationTypeService.UpdateAsync(id, request);

        if (updatedLocationType is null)
        {
            return NotFound();
        }

        return Ok(updatedLocationType);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deletedLocationType  = await _locationTypeService.DeleteAsync(id);

        if (deletedLocationType is null)
        {
            return NotFound();
        }

        return Ok(deletedLocationType);
    }
}
