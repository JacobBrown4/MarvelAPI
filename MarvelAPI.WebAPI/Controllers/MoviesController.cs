using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MarvelAPI.Data;
using MarvelAPI.Data.Entities;
using MarvelAPI.Services.MoviesService;
using MarvelAPI.Models.Movies;

namespace MarvelAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _service;
        private readonly AppDbContext _dbContext;

        public MoviesController(IMoviesService service, AppDbContext dbContext)
        {
            _service = service;
            _dbContext = dbContext;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMoviesAsync([FromBody] MoviesEntity model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createMovies = await _service.CreateMoviesAsync(model);
            if (createMovies)
            {
                return Ok("Movie was added to Database.");
            }
            return BadRequest("Movie could not be added to Database.");
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MoviesDetail>),200)]
        public async Task<IEnumerable<MoviesEntity>> GetAllMoviesAsync()
        {
            var movies = await _service.GetAllMoviesAsync();
            return movies;
        }

        [HttpPut("{moviesId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMoviesAsync([FromBody] MoviesEntity model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest (ModelState);
            }
            if (await _service.UpdateMoviesAsync(model))
            {
                return Ok("Movie was updated successfully.");
            }
            return BadRequest("Could not update Movie.");
        }

        [HttpDelete("{moviesId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteMoviesAsync([FromRoute] int moviesId)
        {
            return await _service.DeleteMoviesAsync(moviesId) ?
            Ok($"The Movie {moviesId} was successfully deleted."):
            BadRequest($"The Movie {moviesId} could not be deleted.");
        }
    }
}