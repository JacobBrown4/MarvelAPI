using MarvelAPI.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using MarvelAPI.Models.TVShowAppearance;
using MarvelAPI.Services.TVShowAppearance;

namespace MarvelAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TVShowAppearanceController : ControllerBase
    {
        private readonly ITVShowAppearanceService _service;
        public TVShowAppearanceController(ITVShowAppearanceService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTVShowAppearanceAsync([FromBody] TVShowAppearanceCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _service.CreateTVShowAppearanceAsync(model))
            {
                return Ok("The TV show appearance has been created and added to database.");
            }
<<<<<<< HEAD
            return BadRequest("TV Show Appearance could not be added to the Database.");
=======
            return BadRequest("Sorry, the TV show appearance could not be created.");
>>>>>>> 8df297afbc9912c596fe2dbf5f2364b29d0cfb42
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TVShowAppearanceListItem>),200)]
        public async Task<IActionResult> GetAllTVShowAppearanceAsync()
        {
            return Ok(await _service.GetAllTVShowAppearancesAsync());
        }

        [HttpGet("{tvShowAppearanceId:int}")]
        [ProducesResponseType(typeof(TVShowAppearanceDetail),200)]
        public async Task<IActionResult> GetTVShowAppearanceByIdAsync([FromRoute] int tvShowAppearanceId)
        {
            var tvShowAppearance = await _service.GetTVShowAppearanceByIdAsync(tvShowAppearanceId);
            if (tvShowAppearance == default)
            {
                return NotFound();
            }
            return Ok(tvShowAppearance);
        }

        [HttpPut("{tvShowAppearanceId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTVShowAppearanceAsync([FromRoute] int tvShowAppearanceId, [FromBody] TVShowAppearanceUpdate request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _service.UpdateTVShowAppearanceAsync(tvShowAppearanceId, request))
            {
                return Ok("The TV show appearance has been updated successfully.");
            }
            return BadRequest("Sorry,the movie appearance could not be updated.");
        }

        [HttpDelete("{tvShowAppearanceId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTVShowAppearanceAsync([FromRoute] int tvShowAppearanceId)
        {
            return await _service.DeleteTVShowAppearanceAsync(tvShowAppearanceId) ? Ok($"The TV show appearance with ID {tvShowAppearanceId} was deleted successfully.") : BadRequest($"Sorry, the TV show appearance with ID {tvShowAppearanceId} could not be deleted.");
        }
    }
}