using MarvelAPI.Data;
using MarvelAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarvelAPI.Services.TVShowsService
{
    public class TVShowsService : ITVShowsService
    {
        private readonly AppDbContext _dbContext;
        public TVShowsService (AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateTVShowsAsync (TVShowsEntity request)
        {
            var tvShowsEntity = new TVShowsEntity
            {
                Title = request.Title,
                ReleaseYear = request.ReleaseYear,
                Seasons = request.Seasons
            };
            _dbContext.TVShows.Add(tvShowsEntity);
            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<TVShowsEntity>> GetAllTVShowsAsync()
        {
            var tvShows = await _dbContext.TVShows
            .ToListAsync();
            return tvShows;
        }

        public async Task<bool> UpdateTVShowsAsync(TVShowsEntity request)
        {
            var tvShowsFound = await _dbContext.TVShows.FindAsync(request.Id);
            if (tvShowsFound is null)
            {
                return false;
            }
            tvShowsFound.Title = request.Title;
            tvShowsFound.ReleaseYear = request.ReleaseYear;
            tvShowsFound.Seasons = request.Seasons;
            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteTVShowsAsync(int Id)
        {
            var tvShowsDelete = await _dbContext.TVShows.FindAsync(Id);
            _dbContext.TVShows.Remove(tvShowsDelete);
            return await _dbContext.SaveChangesAsync() == 1;
        }
    }
}