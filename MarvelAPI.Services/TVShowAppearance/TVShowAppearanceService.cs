using MarvelAPI.Data;
using MarvelAPI.Data.Entities;
using MarvelAPI.Models.TVShowAppearance;
using Microsoft.EntityFrameworkCore;

namespace MarvelAPI.Services.TVShowAppearance
{
    public class TVShowAppearanceService : ITVShowAppearanceService
    {
        private readonly AppDbContext _dbContext;
        public TVShowAppearanceService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateTVShowAppearanceAsync(TVShowAppearanceCreate model)
        {
            var tvShowAppearance = new TVShowAppearanceEntity
            {
                CharacterId = model.CharacterId,
                TVShowId = model.TVShowId
            };

            _dbContext.TVShowAppearance.Add(tvShowAppearance);
            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<TVShowAppearanceListItem>> GetAllTVShowAppearancesAsync()
        {
            var tvShowAppearance = await _dbContext.TVShowAppearance
            .Select(
                x => new TVShowAppearanceListItem
                {
                    Id = x.Id,
                    Character = x.Character.FullName,
                    TVShow = x.TVShow.Title
                }
            )
            .ToListAsync();
            return tvShowAppearance;
        }

        public async Task<TVShowAppearanceDetail> GetTVShowAppearanceByIdAsync(int tvShowAppearanceId)
        {
            var tvShowAppearance = await _dbContext.TVShowAppearance
            .Select(
                x => new TVShowAppearanceDetail
                {
                    Id = x.Id,
                    CharacterId = x.CharacterId,
                    Character = x.Character.FullName,
                    TVShowId = x.TVShowId,
                    TVShow = x.TVShow.Title
                }
            )
            .Where(
                x => x.Id == tvShowAppearanceId
            )
            .FirstOrDefaultAsync();
            return tvShowAppearance;
        }

        public async Task<bool> UpdateTVShowAppearanceAsync(int tvShowAppearanceId, TVShowAppearanceUpdate request)
        {
            var tvShowAppearance = await _dbContext.TVShowAppearance.FindAsync(tvShowAppearanceId);

            if (tvShowAppearance is null)
            {
                return false;
            }
            tvShowAppearance.CharacterId = request.CharacterId;
            tvShowAppearance.TVShowId = request.TVShowId;

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteTVShowAppearanceAsync(int tvShowAppearanceId)
        {
            var tvShowAppearance = await _dbContext.TVShowAppearance.FindAsync(tvShowAppearanceId);
            if (tvShowAppearance is null) 
            {
                return false;
            }
            _dbContext.TVShowAppearance.Remove(tvShowAppearance);
            return await _dbContext.SaveChangesAsync() == 1;
        }
    }
}