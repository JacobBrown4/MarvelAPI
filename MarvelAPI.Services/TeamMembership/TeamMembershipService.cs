using MarvelAPI.Data;
using MarvelAPI.Data.Entities;
using MarvelAPI.Models.TeamMemberships;
using Microsoft.EntityFrameworkCore;

namespace MarvelAPI.Services.TeamMembership
{
    public class TeamMembershipService : ITeamMembershipService
    {
        private readonly AppDbContext _dbContext;
        public TeamMembershipService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateTeamMembershipAsync(TeamMembershipCreate model)
        {
            var teamMembership = new TeamMembershipEntity
            {
                TeamId = model.TeamId,
                MemberId = model.MemberId
            };

            _dbContext.TeamMemberships.Add(teamMembership);
            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<TeamMembershipListItem>> GetAllTeamMembershipsAsync()
        {
            var teamMembership = await _dbContext.TeamMemberships
            .Select(
                x => new TeamMembershipListItem
                {
                    Id = x.Id,
                    TeamId = x.TeamId,
                    MemberId = x.MemberId
                }
            )
            .ToListAsync();
            return teamMembership;
        }

        public async Task<TeamMembershipDetail> GetTeamMembershipByIdAsync(int teamMembershipId)
        {
            var teamMembership = await _dbContext.TeamMemberships
            .Select(
                x => new TeamMembershipDetail
                {
                    Id = x.Id,
                    Team = x.Team.Name,
                    TeamId = x.TeamId,
                    Member = x.Member.FullName,
                    MemberId = x.MemberId
                }
            )
            .Where(
                x => x.Id == teamMembershipId
            )
            .FirstOrDefaultAsync();
            return teamMembership;
            
        }

        public async Task<bool> UpdateTeamMembershipAsync(int teamMembershipId, TeamMembershipUpdate request)
        {
            var teamMembership = await _dbContext.TeamMemberships.FindAsync(teamMembershipId);

            if (teamMembership is null)
            {
                return false;
            }
            teamMembership.MemberId = request.MemberId;
            teamMembership.TeamId = request.TeamId;

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteTeamMembershipAsync(int teamMembershipId)
        {
            var teamMembership = await _dbContext.TeamMemberships.FindAsync(teamMembershipId);
            if (teamMembership is null) 
            {
                return false;
            }
            _dbContext.TeamMemberships.Remove(teamMembership);
            return await _dbContext.SaveChangesAsync() == 1;
        }
    }
}