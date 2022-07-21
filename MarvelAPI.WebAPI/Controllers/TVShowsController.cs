using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MarvelAPI.Models.TVShows;
using MarvelAPI.Data.Entities;
using MarvelAPI.Services.TVShowsService;

namespace MarvelAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TVShowsController : ControllerBase
    {
        private readonly ITVShowsService _service;

        public TVShowsController(ITVShowsService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(TVShowsCreate), 200)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMoviesAsync([FromBody] TVShowsCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createTVShows = await _service.CreateTVShowsAsync(model);
            if (createTVShows)
            {
                return Ok("TV Show was created and added to Database.");
            }
            return BadRequest("TV Show could not be added to Database.");
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TVShowsListItem>), 200)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTVShowsAsync()
        {
            return Ok(await _service.GetAllTVShowsAsync());
        }

        [HttpGet("{Id:int}")]
        [ProducesResponseType(typeof(TVShowsDetail), 200)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTVShowsByIdAsync([FromRoute] int Id)
        {
            var tvShowId = await _service.GetTVShowsByIdAsync(Id);
            if (tvShowId == default)
            {
                return NotFound("Could not find TV Show.");
            }
            return Ok(tvShowId);
        }

        [HttpGet("{tvShowTitle}")]
        [ProducesResponseType(typeof(TVShowsDetail), 200)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTVShowsByTitleAsync([FromRoute] string Title)
        {
            var tvShowTitle = await _service.GetTVShowsByTitleAsync(Title);
            if (tvShowTitle == default)
            {
                return NotFound("Could not find TV Show.");
            }
            return Ok(tvShowTitle);
        }


        [HttpPut("{tvShowsId:int}")]
        [ProducesResponseType(typeof(TVShowsDetail), 200)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTVShowsAsync([FromRoute] int tvShowId, [FromBody] TVShowsUpdate update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest (ModelState);
            }
            if (await _service.UpdateTVShowsAsync(tvShowId, update))
            {
                return Ok("TV Show was updated successfully.");
            }
            return BadRequest("Could not update TV Show.");
        }

        [HttpDelete("{tvShowsId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteTVShowsAsync([FromRoute] int tvShowsId)
        {
            return await _service.DeleteTVShowsAsync(tvShowsId) ?
            Ok($"The TV Show {tvShowsId} was successfully deleted."):
            BadRequest($"The TV Show {tvShowsId} could not be deleted.");
        }
    }
}