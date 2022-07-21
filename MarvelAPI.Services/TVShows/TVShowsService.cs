using MarvelAPI.Data;
using MarvelAPI.Data.Entities;
using MarvelAPI.Models.TVShows;
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

        public async Task<IEnumerable<TVShowsListItem>> GetAllTVShowsAsync()
        {
            var tvShowList = await _dbContext.TVShows.ToListAsync();
            var allTvShow = new List<TVShowsListItem>();
            foreach (var tvS in tvShowList)
            {
                allTvShow.Add(
                    new TVShowsListItem
                    {
                        Id = tvS.Id,
                        Title = tvS.Title
                    }
                );
            }
            return allTvShow;
        }
        public async Task<TVShowsDetail> GetTVShowsByIdAsync(int Id)
        {
            var tvShow = await _dbContext.TVShows.FirstOrDefaultAsync(tvShow => tvShow.Id == Id);
            var tvShowId = new TVShowsDetail
            {
                Id = tvShow.Id,
                Title = tvShow.Title,
                ReleaseYear = (int)tvShow.ReleaseYear,
                Seasons = (int)tvShow.Seasons
            };
            return tvShowId;
        }

        public async Task<bool> UpdateTVShowsAsync(TVShowsUpdate update)
        {
            var tvShowsFound = await _dbContext.TVShows.FindAsync(update.Id);
            if (tvShowsFound is null)
            {
                return false;
            }
            tvShowsFound.Title = update.Title;
            tvShowsFound.ReleaseYear = update.ReleaseYear;
            tvShowsFound.Seasons = update.Seasons;
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