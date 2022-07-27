using MarvelAPI.Models.TeamMemberships;

namespace MarvelAPI.Services.TeamMembership
{
    public interface ITeamMembershipService
    {
        Task<bool> CreateTeamMembershipAsync(TeamMembershipCreate model);
        Task<IEnumerable<TeamMembershipListItem>> GetAllTeamMembershipsAsync();
        Task<TeamMembershipDetail> GetTeamMembershipByIdAsync(int teamMembershipId);
        Task<bool> UpdateTeamMembershipAsync(int teamMembershipId, TeamMembershipUpdate request);
        Task<bool> DeleteTeamMembershipAsync(int teamMembershipId);
    }
}