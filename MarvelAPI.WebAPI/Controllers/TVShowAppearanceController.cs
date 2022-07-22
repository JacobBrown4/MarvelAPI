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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTVShowAppearanceAsync([FromBody] TVShowAppearanceCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createTVShowAppearance = await _service.CreateTVShowAppearanceAsync(model);

            if (createTVShowAppearance)
            {
                return Ok("TV Show Appearance was added to Database.");
            }
            return BadRequest("TV Show Appearance could not be added to the Database.");
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TVShowAppearanceDetail>),200)]
        public async Task<IEnumerable<TVShowAppearanceEntity>> GetAllTVShowAppearanceAsync()
        {
            var tvShowAppearance = await _service.GetAllTVShowAppearanceAsync();
            return tvShowAppearance;
        }

        [HttpPut("{tvShowAppearanceId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTVShowAppearanceAsync([FromRoute] int Id)
        {
            var requestTVShowAppearance = await _service.GetTVShowAppearanceDetailByIdAsync(Id);
            var updatedTVShowAppearance = await _service.UpdateTVShowAppearanceAsync(requestTVShowAppearance);
            if (updatedTVShowAppearance is false)
            {
                return NotFound();
            }
            return Ok(updatedTVShowAppearance);
        }

        [HttpDelete("{tvShowAppearanceId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTVShowAppearanceAsync([FromRoute] int Id)
        {
            var tvShowAppearanceToDelete = await _service.DeleteTVShowAppearanceAsync(Id);
            if (tvShowAppearanceToDelete is false)
            {
                return NotFound();
            }
            return Ok(tvShowAppearanceToDelete);
        }
    }
}