using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MarvelAPI.Services.Character;
using MarvelAPI.Models.Characters;

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

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(typeof(CharacterListItem), 200)]
        public async Task<IActionResult> GetAllCharactersAsync() {
            return Ok(await _service.GetAllCharactersAsync());
        }

        [HttpGet("{characterId:int}")]
        [ProducesResponseType(typeof(CharacterDetail), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCharacterByIdAsync([FromRoute] int characterId) {
            var character = await _service.GetCharacterByIdAsync(characterId);
            if (character == default) {
                return NotFound();
            }
            return Ok(character);
        }

        [HttpGet("Abilities/{ability}")]
        [ProducesResponseType(typeof(CharacterAbilities), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCharactersByAbilityAsync([FromRoute] string ability) {
            var result = await _service.GetCharactersByAbilityAsync(ability);
            if (result == default) {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("Aliases/{aliases}")]
        [ProducesResponseType(typeof(CharacterAliases), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCharactersByAliasesAsync([FromRoute] string aliases) {
            var result = await _service.GetCharactersByAliasesAsync(aliases);
            if (result == default) {
                return NotFound();
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPut("{characterId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCharacterAsync([FromRoute] int characterId, [FromBody] CharacterUpdate model) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _service.UpdateCharacterAsync(characterId, model)) {
                return Ok("Character updated successfully.");
            }
            return BadRequest("Could not update character.");
        }

        [Authorize]
        [HttpDelete("{characterId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCharacterByIdAsync([FromRoute] int characterId) {
            return await _service.DeleteCharacterAsync(characterId) ?
            Ok($"Character with ID {characterId} deleted successfully.") :
            BadRequest($"Character with ID {characterId} could not be deleted.");
        }
    }
}