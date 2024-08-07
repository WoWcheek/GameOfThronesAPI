using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BLL.DTOs.LivingCreatures.Requests;

namespace Presentation.Controllers.LivingCreatures;

[Route("/api/[controller]")]
[ApiController]
public class GendersController : ControllerBase
{
    private readonly IGenderService _genderService;

    public GendersController(IGenderService genderService)
    {
        _genderService = genderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var genders = await _genderService.GetAllAsync();

        return Ok(genders);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var gender = await _genderService.GetByIdAsync(id);

        if (gender is null)
        {
            return NotFound();
        }

        return Ok(gender);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddGenderDTO request)
    {
        var adddedGender = await _genderService.AddAsync(request);

        return CreatedAtAction(nameof(GetById), new { id = adddedGender.Id }, adddedGender);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateGenderDTO request)
    {
        var updatedGender = await _genderService.UpdateAsync(id, request);

        if (updatedGender is null)
        {
            return NotFound();
        }

        return Ok(updatedGender);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deletedGender = await _genderService.DeleteAsync(id);

        if (deletedGender is null)
        {
            return NotFound(); 
        }

        return Ok(deletedGender);
    }
}