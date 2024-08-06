using BLL.Interfaces;
using BLL.DTOs.Houses.Requests;
using Microsoft.AspNetCore.Mvc;

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
        var addedHouse = await _houseService.AddAsync(request);

        return CreatedAtAction(nameof(GetById), new { id = addedHouse.Id }, addedHouse);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateHouseDTO request)
    {
        var updatedHouse = await _houseService.UpdateAsync(id, request);

        if (updatedHouse is null)
        {
            return NotFound();
        }

        return Ok(updatedHouse);
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