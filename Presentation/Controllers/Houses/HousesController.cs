using BLL.Interfaces;
using BLL.DTOs.Houses.Requests;
using Microsoft.AspNetCore.Mvc;
using BLL.Exceptions;

namespace Presentation.Controllers.Houses;

[Route("/api/[controller]")]
[ApiController]
public class HousesController : ControllerBase
{
    private readonly IHouseService _houseService;

    public HousesController(IHouseService houseService)
    {
        _houseService = houseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var houses = await _houseService.GetAllAsync();

        return Ok(houses);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var house = await _houseService.GetByIdAsync(id);

        if (house is null)
        {
            return NotFound();
        }

        return Ok(house);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddHouseDTO request)
    {
        try
        {
            var addedHouse = await _houseService.AddAsync(request);

            return CreatedAtAction(nameof(GetById), new { id = addedHouse.Id }, addedHouse);
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
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateHouseDTO request)
    {
        try
        {
            var updatedHouse = await _houseService.UpdateAsync(id, request);

            if (updatedHouse is null)
            {
                return NotFound();
            }

            return Ok(updatedHouse);
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
        var deletedHouse = await _houseService.DeleteAsync(id);

        if (deletedHouse is null)
        {
            return NotFound();
        }

        return Ok(deletedHouse);
    }
}