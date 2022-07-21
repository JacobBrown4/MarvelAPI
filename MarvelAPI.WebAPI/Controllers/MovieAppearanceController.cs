using MarvelAPI.Models.MovieAppearance;
using MarvelAPI.Services.MovieAppearance;
using Microsoft.AspNetCore.Mvc;

namespace MarvelAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieAppearanceController : ControllerBase
    {
        private readonly IMovieAppearanceService _service;
        public MovieAppearanceController(IMovieAppearanceService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovieAppearanceAsync([FromBody] MovieAppearanceCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _service.CreateMovieAppearanceAsync(model)) 
            {
                return Ok("The movie appearance was created and added to the database successfully.");
            }
            return BadRequest("Sorry, the movie appearance could not be created.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovieAppearancesAsync()
        {
            return Ok(await _service.GetAllMovieAppearancesAsync());
        }

        [HttpGet("{movieAppearanceId:int}")]
        public async Task<IActionResult> GetMovieAppearanceByIdAsync([FromRoute] int movieAppearanceId)
        {
            var movieAppearance = await _service.GetMovieAppearanceByIdAsync(movieAppearanceId);
            if (movieAppearance == default) 
            {
                return NotFound();
            }
            return Ok(movieAppearance);
        }

        [HttpPut("{movieAppearanceId:int}")]
        public async Task<IActionResult> UpdateMovieAppearanceAsync([FromRoute] int movieAppearanceId,[FromBody] MovieAppearanceUpdate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _service.UpdateMovieAppearanceAsync(movieAppearanceId, model))
            {
                return Ok("The movie appearance has been updated successfully.");
            }
            return BadRequest("Sorry, the movie appearnce could be updated.");
        }

        [HttpDelete("{movieAppearanceId:int}")]
        public async Task<IActionResult> DeleteMovieAppearanceAsync([FromRoute] int movieAppearanceId)
        {
            return await _service.DeleteMovieAppearanceAsync(movieAppearanceId) ? Ok($"The movie appearance with ID {movieAppearanceId} was deleted successfully.") : BadRequest($"Sorry, the movie appearance with ID {movieAppearanceId} could not be deleted.");
        }
    }
}
