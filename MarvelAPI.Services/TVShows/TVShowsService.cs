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

        public async Task<bool> CreateTVShowsAsync (TVShows request)
        {
            var tvShowsEntity = new TVShows
            {
                Title = request.Title,
                ReleaseYear = request.ReleaseYear
            };
            _dbContext.TVShows.Add(tvShowsEntity);
            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<TVShows>> GetAllTVShowsAsync()
        {
            var tvShows = await _dbContext.TVShows
            .ToListAsync();
            return tvShows;
        }

        public async Task<bool> UpdateTVShowsAsync(TVShows request)
        {
            var tvShowsEntity = await _dbContext.TVShows.FindAsync(request.Id);
            tvShowsEntity.Title = request.Title;
            tvShowsEntity.ReleaseYear = request.ReleaseYear;
            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteTVShowsAsync(int Id)
        {
            var tvShowsEntity = await _dbContext.TVShows.FindAsync(Id);
            _dbContext.TVShows.Remove(tvShowsEntity);
            return await _dbContext.SaveChangesAsync() == 1;
        }
    }
}