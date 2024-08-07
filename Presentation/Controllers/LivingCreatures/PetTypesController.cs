using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BLL.DTOs.LivingCreatures.Requests;

namespace Presentation.Controllers.LivingCreatures;

[Route("/api/[controller]")]
[ApiController]
public class PetTypesController : ControllerBase
{
    private readonly IPetTypeService _petTypeService;

    public PetTypesController(IPetTypeService petTypeService)
    {
        _petTypeService = petTypeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var petTypes = await _petTypeService.GetAllAsync();

        return Ok(petTypes);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var petType = await _petTypeService.GetByIdAsync(id);

        if (petType is null)
        {
            return NotFound();
        }

        return Ok(petType);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddPetTypeDTO request)
    {
        var addedPetType = await _petTypeService.AddAsync(request);

        return CreatedAtAction(nameof(GetById), new { id = addedPetType.Id }, addedPetType);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatePetTypeDTO request)
    {
        var updatedPetType = await _petTypeService.UpdateAsync(id, request);

        if (updatedPetType is null)
        {
            return NotFound();
        }

        return Ok(updatedPetType);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deletedPetType = await _petTypeService.DeleteAsync(id);

        if (deletedPetType is null)
        {
            return NotFound();
        }

        return Ok(deletedPetType);
    }
}
