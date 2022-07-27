using MarvelAPI.Data;
using MarvelAPI.Data.Entities;
using MarvelAPI.Models.Teams;
using MarvelAPI.Models.Characters;
using Microsoft.EntityFrameworkCore;

namespace MarvelAPI.Services.Teams
{
    public class TeamsService : ITeamsService
    {
        private readonly AppDbContext _dbContext;
        public TeamsService (AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateTeamAsync(TeamCreate model)
        {
            var team = new TeamEntity
            {
                Name = model.Name
            };
            foreach (var t in await _dbContext.Teams.ToListAsync())
            {
                if (ReformatTeam(t.Name) == ReformatTeam(team.Name))
                {
                    return false;
                }
            }
            _dbContext.Teams.Add(team);
            var numOfChanges = await _dbContext.SaveChangesAsync();
            return numOfChanges == 1;
        }

        public async Task<IEnumerable<TeamListItem>> GetAllTeamsAsync()
        {
            var result = await _dbContext.Teams
                .Select(
                    t => new TeamListItem
                    {
                        Id = t.Id,
                        Name = t.Name
                    }
                ).ToListAsync();
            return result;
        }

        public async Task<TeamDetail> GetTeamByIdAsync(int Id)
        {
            var teamFound = await _dbContext.Teams.FindAsync(Id);
            var teamMember = teamFound.Members.Select
            (
                m => new CharacterListItem
                {
                    Id = m.Id,
                    FullName = m.Member.FullName
                }
            );
            var result = new TeamDetail{
                Id = teamFound.Id,
                Name = teamFound.Name,
                Authority = teamFound.Authority,
                Alignment = teamFound.Alignment,
                Members = teamMember.ToList()
            };
            return result;
        }

        public async Task<bool> UpdateTeamAsync(int teamId, TeamUpdate request)
        {
            var teamFound = await _dbContext.Teams.FindAsync(teamId);
            if (teamFound is null)
            {
                return false;
            }
            teamFound.Name = CheckUpdateProperty(teamFound.Name, request.Name);
            teamFound.Authority = CheckUpdateProperty(teamFound.Authority, request.Authority);
            teamFound.Alignment = CheckUpdateProperty(teamFound.Alignment, request.Alignment);
            var numOfChanges = await _dbContext.SaveChangesAsync();
            return numOfChanges == 1;
        }

        public async Task<bool> DeleteTeamAsync(int Id)
        {
            var team = await _dbContext.Teams.FindAsync(Id);
            _dbContext.Teams.Remove(team);
            return await _dbContext.SaveChangesAsync() == 1;
        }

        private string ReformatTeam(string name)
        {
            var result = String.Concat(name.Split(' ', '-')).ToLower();
            return result;
        }
        private string CheckUpdateProperty(string from, string to)
        {
            return String.IsNullOrEmpty(to.Trim()) ? from : to.Trim();
        }
    }
}