using System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarvelAPI.Data;
using MarvelAPI.Data.Entities;
using MarvelAPI.Models.MovieAppearance;
using MarvelAPI.Services.MovieAppearance;
using Microsoft.AspNetCore.Http;
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

            var createMovieAppearance = await _service.CreateMovieAppearanceAsync(model);

            if (createMovieAppearance)
            {
                return Ok("Move Appearance was added to the databse.");
            }
            return BadRequest("Movie Appearance could not be added to the database.");
        }

        [HttpGet]
        public async Task<IEnumerable<MovieAppearanceListItem>> GetAllMovieAppearancesAsync()
        {
            var movieAppearances = await _service.GetAllMovieAppearancesAsync();

            return movieAppearances;
        }

        [HttpPut("{movieAppearanceId:int}")]
        public async Task<IActionResult> UpdateMovieAppearanceAsync([FromRoute] int id)
        {
            var requestMovieAppearance = await _service.GetMovieAppearanceDetailByIdAsync(id);

            var updateMovieAppearance = await _service.UpdateMovieAppearanceAsync(requestMovieAppearance);

            if (updateMovieAppearance is false)
            {
                return NotFound();
            }
            return Ok(updateMovieAppearance);

        }

        [HttpDelete("{movieAppearanceId:int}")]
        public async Task<IActionResult> DeleteMovieAppearanceAsync(int id)
        {
            var movieAppearanceToDelete = await _service.DeleteMovieAppearanceAsync(id);

            if (movieAppearanceToDelete is false)
            {
                return NotFound();
            }
            return Ok(movieAppearanceToDelete);
        }
}
}
