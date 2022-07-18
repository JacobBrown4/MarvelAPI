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
        public async Task<bool> CreateTVShowAppearanceAsync(TVShowAppearanceCreate request)
        {
            var entity = new TVShowAppearanceEntity
            {
                CharacterId = request.CharacterId,
                TVShowId = request.TVShowId
            };

            _dbContext.TVShowAppearance.Add(entity);

            var numberOfChanges = await _dbContext.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<TVShowAppearanceEntity>> GetAllTVShowAppearanceAsync()
        {
            var tvShowAppearance = await _dbContext.TVShowAppearance

            .ToListAsync();

            return tvShowAppearance;
        }

        public async Task<TVShowAppearanceDetail> GetTVShowAppearanceDetailByIdAsync(int Id)
        {
            var tvShowAppearanceEntity = await _dbContext.TVShowAppearance.FirstOrDefaultAsync(e => e.Id == Id);

            return tvShowAppearanceEntity is null ? null : new TVShowAppearanceDetail
            {
                Id = tvShowAppearanceEntity.Id,
                TVShowId = tvShowAppearanceEntity.TVShowId,
                CharacterId = tvShowAppearanceEntity.CharacterId
            };
        }

        public async Task<bool> UpdateTVShowAppearanceAsync(TVShowAppearanceDetail request)
        {
            var tvShowAppearanceEntity = await _dbContext.TVShowAppearance.FindAsync(request.Id);
            tvShowAppearanceEntity.CharacterId = request.CharacterId;
            tvShowAppearanceEntity.TVShowId = request.TVShowId;

            var numberOfChanges = await _dbContext.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteTVShowAppearanceAsync(int id)
        {
            var tvShowAppearanceEntity = await _dbContext.TVShowAppearance.FindAsync(id);

            _dbContext.TVShowAppearance.Remove(tvShowAppearanceEntity);

            return await _dbContext.SaveChangesAsync() == 1;
        }
    }
}