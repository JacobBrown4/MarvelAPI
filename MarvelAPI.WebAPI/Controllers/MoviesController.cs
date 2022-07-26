using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MarvelAPI.Services.MoviesService;
using MarvelAPI.Models.Movies;

namespace MarvelAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _service;

        public MovieController(IMovieService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMoviesAsync([FromBody] MovieCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _service.CreateMoviesAsync(model))
            {
                return Ok("The movie has been created and added to the database.");
            }
            return BadRequest("Sorry, the movie could not be created.");
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MovieListItem>),200)]
        public async Task<IActionResult> GetAllMoviesAsync()
        {
            return Ok(await _service.GetAllMoviesAsync());
        }

        [HttpGet("{movieId:int}")]
        [ProducesResponseType(typeof(MovieDetail), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMovieByIdAsync([FromRoute] int movieId)
        {
            var movie = await _service.GetMovieByIdAsync(movieId);
            if (movie == default)
            {
                return NotFound("Sorry, the movie could not be found.");
            }
            return Ok(movie);
        }

        [HttpGet("{movieTitle}")]
        [ProducesResponseType(typeof(MovieDetail), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMovieByTitleAsync([FromRoute] string movieTitle)
        {
            var movie = await _service.GetMovieByTitleAsync(movieTitle);
            if (movie == default)
            {
                return NotFound("Sorry, the movie could not be found.");
            }
            return Ok(movie);
        }

        [Authorize]
        [HttpPut("{movieId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMoviesAsync([FromRoute] int movieId, [FromBody] MovieUpdate request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _service.UpdateMoviesAsync(movieId, request))
            {
                return Ok("The movie has been updated successfully.");
            }
            return BadRequest("Sorry, the movie could not be updated.");
        }

        [Authorize]
        [HttpDelete("{movieId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteMoviesAsync([FromRoute] int movieId)
        {
            return await _service.DeleteMoviesAsync(movieId) ?
            Ok($"The movie with ID {movieId} was deleted successfully."):
            BadRequest($"The movie with ID {movieId} could not be deleted.");
        }
    }
}