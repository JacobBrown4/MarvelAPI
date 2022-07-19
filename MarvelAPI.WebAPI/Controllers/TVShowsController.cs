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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTVShowsAsync([FromRoute] int Id)
        {
            var requestTVShows = await _dbContext.TVShows.FindAsync(Id);
            var updatedTVShows = await _service.UpdateTVShowsAsync(requestTVShows);
            if (updatedTVShows is false)
            {
                return NotFound();
            }
            return Ok(updatedTVShows);
        }

        [HttpDelete("{tvShowsId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTVShowsAsync([FromRoute] int Id)
        {
            var tvShowsToDelete = await _service.DeleteTVShowsAsync(Id);
            if (tvShowsToDelete is false)
            {
                return NotFound();
            }
            return Ok(tvShowsToDelete);
        }
    }
}