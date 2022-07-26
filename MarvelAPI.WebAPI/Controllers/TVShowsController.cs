using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MarvelAPI.Models.TVShows;
using MarvelAPI.Services.TVShowsService;

namespace MarvelAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TVShowController : ControllerBase
    {
        private readonly ITVShowService _service;

        public TVShowController(ITVShowService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTvShowAsync([FromBody] TVShowCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _service.CreateTVShowAsync(model))
            {
                return Ok("The TV show has been created and added to Database.");
            }
            return BadRequest("Sorry, the TV show could not be created.");
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TVShowListItem>), 200)]
        public async Task<IActionResult> GetAllTVShowsAsync()
        {
            return Ok(await _service.GetAllTVShowsAsync());
        }

        [HttpGet("{tvShowId:int}")]
        [ProducesResponseType(typeof(TVShowDetail), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTVShowByIdAsync([FromRoute] int tvShowId)
        {
            var tvShow = await _service.GetTVShowByIdAsync(tvShowId);
            if (tvShow == default)
            {
                return NotFound("Sorry, the TV show could not be found.");
            }
            return Ok(tvShow);
        }

        [HttpGet("{tvShowTitle}")]
        [ProducesResponseType(typeof(TVShowDetail), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTVShowByTitleAsync([FromRoute] string tvShowTitle)
        {
            var tvShow = await _service.GetTVShowByTitleAsync(tvShowTitle);
            if (tvShow == default)
            {
                return NotFound("Sorry, the TV show could not be found.");
            }
            return Ok(tvShow);
        }

        [Authorize]
        [HttpPut("{tvShowId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTVShowAsync([FromRoute] int tvShowId, [FromBody] TVShowUpdate request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest (ModelState);
            }
            if (await _service.UpdateTVShowAsync(tvShowId, request))
            {
                return Ok("The TV show has been updated successfully.");
            }
            return BadRequest("Sorry, the TV show could not be updated.");
        }

        [Authorize]
        [HttpDelete("{tvShowId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteTVShowAsync([FromRoute] int tvShowId)
        {
            return await _service.DeleteTVShowAsync(tvShowId) ?
            Ok($"The TV show with ID {tvShowId} was deleted successfully."):
            BadRequest($"Sorry, the TV show with ID {tvShowId} could not be deleted. Verify the TV show Id is accurate.");
        }
    }
}