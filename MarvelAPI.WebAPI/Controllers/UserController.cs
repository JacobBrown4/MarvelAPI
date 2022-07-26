using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MarvelAPI.Services.User;
using MarvelAPI.Models.Users;
using MarvelAPI.Services.Token;
using MarvelAPI.Models.Token;

namespace MarvelAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        public UserController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("~/api/Token")]
        public async Task<IActionResult> Token([FromBody] TokenRequest request) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var tokenResponse = await _tokenService.GetTokenAsync(request);
            if (tokenResponse is null) {
                return BadRequest("Invalid username or password.");
            }
            return Ok(tokenResponse);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] UserRegister model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registerResult = await _userService.RegisterUserAsync(model);
            if (registerResult)
            {
                return Ok("The user was registered.");
            }
            return BadRequest("Sorry, the user could not be registered.");
        }
        
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync() 
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        [Authorize]
        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] int userId)
        {
            var userDetail = await _userService.GetUserByIdAsync(userId);

            if (userDetail is null)
            {
                return NotFound();
            }

            return Ok(userDetail);
        }

        [HttpPut("{userId:int}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] int userId, [FromBody] UserUpdate request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _userService.UpdateUserAsync(userId, request))
            {
                return Ok("The user was updated successfully.");
            }
            return BadRequest("Sorry, the user could not be updated.");
        }

        // [HttpDelete("{userId:int}")]
        // public async Task<IActionResult> DeleteUserAsync([FromRoute] int userId)
        // {

        // }
    }
}
