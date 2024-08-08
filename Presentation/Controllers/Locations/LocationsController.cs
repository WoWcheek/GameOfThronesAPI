using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BLL.DTOs.Locations.Requests;
using BLL.Exceptions;

namespace Presentation.Controllers.Locations;

[Route("/api/[controller]")]
[ApiController]
public class LocationsController : ControllerBase
{
    private readonly ILocationService _locationService;

    public LocationsController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var locations = await _locationService.GetAllAsync();

        return Ok(locations);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var location = await _locationService.GetByIdAsync(id);

        if (location is null)
        {
            return NotFound();
        }

        return Ok(location);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddLocationDTO request)
    {
        try
        {
            var addedLocation = await _locationService.AddAsync(request);

            return CreatedAtAction(nameof(GetById), new { id = addedLocation.Id }, addedLocation);
        }
        catch (ForeignKeyToNonExistentObjectException ex)
        {
            return BadRequest(ex.Message);
        }
        catch
        {
            return BadRequest("Something went wrong.");
        }
    }

    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateLocationDTO request)
    {
        try
        {
            var updatedLocation = await _locationService.UpdateAsync(id, request);

            if (updatedLocation is null)
            {
                return NotFound();
            }

            return Ok(updatedLocation);
        }
        catch (ForeignKeyToNonExistentObjectException ex)
        {
            return BadRequest(ex.Message);
        }
        catch
        {
            return BadRequest("Something went wrong.");
        }
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deletedLocation = await _locationService.DeleteAsync(id);

        if (deletedLocation is null)
        {
            return NotFound();
        }

        return Ok(deletedLocation);
    }
}
