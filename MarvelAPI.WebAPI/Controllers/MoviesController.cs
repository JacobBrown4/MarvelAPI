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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateMoviesAsync([FromRoute] int Id)
        {
            var requestMovies = await _dbContext.Movies.FindAsync(Id);
            var updatedMovies = await _service.UpdateMoviesAsync(requestMovies);
            if (updatedMovies is false)
            {
                return NotFound();
            }
            return Ok(updatedMovies);
        }

        [HttpDelete("{moviesId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMoviesAsync([FromRoute] int Id)
        {
            var moviesToDelete = await _service.DeleteMoviesAsync(Id);
            if (moviesToDelete is false)
            {
                return NotFound();
            }
            return Ok(moviesToDelete);
        }
    }
}