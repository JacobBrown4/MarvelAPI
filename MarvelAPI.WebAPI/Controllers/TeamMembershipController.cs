using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MarvelAPI.Models.TeamMemberships;
using MarvelAPI.Services.TeamMembership;

namespace MarvelAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMembershipController : ControllerBase
    {
        private readonly ITeamMembershipService _service;
        public TeamMembershipController(ITeamMembershipService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTeamMembershipAsync([FromBody] TeamMembershipCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _service.CreateTeamMembershipAsync(model))
            {
                return Ok("The Team Membership has been created and added to database.");
            }
            return BadRequest("Sorry, the Team Membership could not be created.");
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TeamMembershipListItem>),200)]
        public async Task<IActionResult> GetAllTeamMembershipAsync()
        {
            return Ok(await _service.GetAllTeamMembershipsAsync());
        }

        [HttpGet("{teamMembershipId:int}")]
        [ProducesResponseType(typeof(TeamMembershipDetail),200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTeamMembershipByIdAsync([FromRoute] int teamMembershipId)
        {
            var teamMembership = await _service.GetTeamMembershipByIdAsync(teamMembershipId);
            if (teamMembership == default)
            {
                return NotFound();
            }
            return Ok(teamMembership);
        }

        [Authorize]
        [HttpPut("{teamMembershipId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTeamMembershipAsync([FromRoute] int teamMembershipId, [FromBody] TeamMembershipUpdate request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _service.UpdateTeamMembershipAsync(teamMembershipId, request))
            {
                return Ok("The Team Membership has been updated successfully.");
            }
            return BadRequest("Sorry,the Team Membership could not be updated.");
        }

        [Authorize]
        [HttpDelete("{teamMembershipId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteTeamMembershipAsync([FromRoute] int teamMembershipId)
        {
            return await _service.DeleteTeamMembershipAsync(teamMembershipId) ? 
            Ok($"The Team Membership with ID {teamMembershipId} was deleted successfully.") : 
            BadRequest($"Sorry, the Team Membership with ID {teamMembershipId} could not be deleted.");
        }
    }
}