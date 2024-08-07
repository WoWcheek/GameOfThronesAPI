using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BLL.DTOs.LivingCreatures.Requests;
using BLL.Services;

namespace Presentation.Controllers.LivingCreatures;

[Route("/api/[controller]")]
[ApiController]
public class PetsController : ControllerBase
{
    private readonly IPetService _petService;

    public PetsController(IPetService petService)
    {
        _petService = petService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var pets = await _petService.GetAllAsync();

        return Ok(pets);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var pet = await _petService.GetByIdAsync(id);

        if (pet is null)
        {
            return NotFound();
        }

        return Ok(pet);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddPetDTO request)
    {
        var addedPet = await _petService.AddAsync(request);

        return CreatedAtAction(nameof(GetById), new { id = addedPet.Id }, addedPet);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatePetDTO request)
    {
        var updatedPet = await _petService.UpdateAsync(id, request);

        if (updatedPet is null)
        {
            return NotFound();
        }

        return Ok(updatedPet);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deletedPet = await _petService.DeleteAsync(id);

        if (deletedPet is null)
        {
            return NotFound();
        }

        return Ok(deletedPet);
    }
}