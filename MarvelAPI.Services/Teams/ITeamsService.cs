using MarvelAPI.Models.Teams;

namespace MarvelAPI.Services.Teams
{
    public interface ITeamsService
    {
        Task<bool> CreateTeamAsync(TeamCreate model);
        Task<IEnumerable<TeamListItem>> GetAllTeamsAsync();
        Task<TeamDetail> GetTeamByIdAsync(int Id);
        Task<bool> UpdateTeamAsync(int teamId, TeamUpdate request);
        Task<bool> DeleteTeamAsync(int Id);
    }
}