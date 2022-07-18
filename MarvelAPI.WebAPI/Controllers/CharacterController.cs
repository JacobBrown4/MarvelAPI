using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MarvelAPI.Services.Character;
using MarvelAPI.Models.Characters;
using MarvelAPI.Data.Entities;

namespace MarvelAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _service;
        public CharacterController(ICharacterService characterService) {
            _service = characterService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCharacterAsync([FromBody] CharacterCreate model) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _service.CreateCharacterAsync(model)) {
                return Ok("Character created successfully.");
            }
            return BadRequest("Could not create character.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCharactersAsync() {
            return Ok(await _service.GetAllCharactersAsync());
        }

        [HttpGet("{characterId:int}")]
        public async Task<IActionResult> GetCharacterByIdAsync([FromBody] int characterId) {
            var character = await _service.GetCharacterByIdAsync(characterId);
            if (character == default) {
                return NotFound();
            }
            return Ok(character);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCharacterAsync([FromBody] CharacterEntity model) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _service.UpdateCharacterAsync(model)) {
                return Ok("Character updated successfully.");
            }
            return BadRequest("Could not update character.");
        }

        [HttpDelete("{characterId:int}")]
        public async Task<IActionResult> DeleteCharacterByIdAsync([FromRoute] int characterId) {
            return await _service.DeleteCharacterAsync(characterId) ?
            Ok($"Character with ID {characterId} deleted successfully.") :
            BadRequest($"Character with ID {characterId} could not be deleted.");
        }
    }
}