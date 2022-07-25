using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MarvelAPI.Services.User;
using MarvelAPI.Models.Users;

namespace MarvelAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUserAsync([FromBody] UserRegister model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registerResult = await _service.RegisterUserAsync(model);
            if (registerResult)
            {
                return Ok("The user was registered.");
            }
            return BadRequest("Sorry, the user could not be registered.");
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync() 
        {
            return Ok(await _service.GetAllUsersAsync());
        }

        [Authorize]
        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] int userId)
        {
            var userDetail = await _service.GetUserByIdAsync(userId);

            if (userDetail is null)
            {
                return NotFound();
            }

            return Ok(userDetail);
        }

        // [HttpPut("{userId:int}")]
        // public async Task<IActionResult> UpdateUserAsync([FromRoute] int userId, [FromBody] UserUpdate request)
        // {

        // }

        // [HttpDelete("{userId:int}")]
        // public async Task<IActionResult> DeleteUserAsync([FromRoute] int userId)
        // {

        // }
    }
}
