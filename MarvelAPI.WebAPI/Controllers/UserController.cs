using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MarvelAPI.Services.User;
using Microsoft.AspNetCore.Authorization;

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

        // [Authorize]
        // [HttpGet("{userId:int}")]
        // public async Task<IActionResult> GetById([FromRoute] int userId) {
        //     // var userDetail = await _service.GetUserByIdAsync(userId);
        // }
    }
}
