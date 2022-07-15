using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MarvelAPI.Services.MovieAppearance;

namespace MarvelAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieAppearanceController : ControllerBase
    {
        private readonly IMovieAppearance _service;
        public MovieAppearanceController(IMovieAppearnceService service)
        {
            _service = service;
        }
    }
}
