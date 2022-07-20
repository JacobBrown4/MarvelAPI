using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MarvelAPI.Data;
using MarvelAPI.Data.Entities;
using MarvelAPI.Services.TVShowsService;

namespace MarvelAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TVShowsController : ControllerBase
    {
        private readonly ITVShowsService _service;
        private readonly AppDbContext _dbContext;

        public TVShowsController(ITVShowsService service, AppDbContext dbContext)
        {
            _service = service;
            _dbContext = dbContext;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMoviesAsync([FromBody] TVShowsEntity model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createTVShows = await _service.CreateTVShowsAsync(model);
            if (createTVShows)
            {
                return Ok("TV Show was added to Database.");
            }
            return BadRequest("TV Show could not be added to Database.");
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<TVShowsEntity>> GetAllTVShowsAsync()
        {
            var tvShows = await _service.GetAllTVShowsAsync();
            return tvShows;
        }

        [HttpPut("{tvShowsId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTVShowsAsync([FromBody] TVShowsEntity model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest (ModelState);
            }
            if (await _service.UpdateTVShowsAsync(model))
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