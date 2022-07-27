using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MarvelAPI.Services.Teams;
using MarvelAPI.Models.Teams;

namespace MarvelAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamsService  _service;
        public TeamsController(ITeamsService teamsService)
        {
            _service = teamsService;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTeamsAsync([FromBody] TeamCreate model) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _service.CreateTeamAsync(model)) {
                return Ok("Team created successfully.");
            }
            return BadRequest("Could not create Team.");
        }

        [HttpGet]
        [ProducesResponseType(typeof(TeamListItem), 200)]
        public async Task<IActionResult> GetAllTeamsAsync() {
            return Ok(await _service.GetAllTeamsAsync());
        }

        [HttpGet("{teamId:int}")]
        [ProducesResponseType(typeof(TeamDetail), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTeamByIdAsync([FromRoute] int teamId) {
            var team = await _service.GetTeamByIdAsync(teamId);
            if (team == default) {
                return NotFound();
            }
            return Ok(team);
        }


        [Authorize]
        [HttpPut("{teamId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTeamAsync([FromRoute] int teamId, [FromBody] TeamUpdate model) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _service.UpdateTeamAsync(teamId, model)) {
                return Ok("Team updated successfully.");
            }
            return BadRequest("Could not update Team.");
        }

        [Authorize]
        [HttpDelete("{teamId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteTeamByIdAsync([FromRoute] int teamId) {
            return await _service.DeleteTeamAsync(teamId) ?
            Ok($"Team with ID {teamId} deleted successfully.") :
            BadRequest($"Team with ID {teamId} could not be deleted.");
        }
        
    }
}