using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BLL.DTOs.LivingCreatures.Requests;

namespace Presentation.Controllers.LivingCreatures;

[Route("/api/[controller]")]
[ApiController]
public class CharactersController : ControllerBase
{
    private readonly ICharacterService _characterService;

    public CharactersController(ICharacterService characterService)
    {
        _characterService = characterService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var characters = await _characterService.GetAllAsync();

        return Ok(characters);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var character = await _characterService.GetByIdAsync(id);

        if (character is null)
        {
            return NotFound();
        }

        return Ok(character);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddCharacterDTO request)
    {
        var addedCharacter = await _characterService.AddAsync(request);

        return CreatedAtAction(nameof(GetById), new { id = addedCharacter.Id }, addedCharacter);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCharacterDTO request)
    {
        var updatedCharacter = await _characterService.UpdateAsync(id, request);

        if (updatedCharacter is null)
        {
            return NotFound();
        }

        return Ok(updatedCharacter);
    }


    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deletedCharacter = await _characterService.DeleteAsync(id);

        if (deletedCharacter is null)
        {
            return NotFound();
        }

        return Ok(deletedCharacter);
    }
}