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

        public async Task<bool> CreateTVShowAsync (TVShowCreate model)
        {
            var tvShowsCreate = new TVShowsEntity
            {
                Title = model.Title,
                ReleaseYear = model.ReleaseYear,
                Seasons = model.Seasons
            };
            foreach (var tvS in await _dbContext.TVShows.ToListAsync())
            {
                if (ReformatCreatedTitle(tvS) == ReformatCreatedTitle(tvShowsCreate))
                {
                    return false;
                }
            }
            _dbContext.TVShows.Add(tvShowsCreate);
            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<TVShowListItem>> GetAllTVShowsAsync()
        {
            var tvShowList = await _dbContext.TVShows.Select(tvS => new TVShowListItem
            {
                Id = tvS.Id,
                Title = tvS.Title
            }).ToListAsync();
            return tvShowList;
        }

        // ! Removed IEnumerable from this since we are not returning a list
        public async Task<TVShowDetail> GetTVShowByIdAsync(int tvShowId)
        {
            var tvShow = await _dbContext.TVShows.Select(tvSId => new TVShowDetail
            {
                Id = tvSId.Id,
                Title = tvSId.Title,
                ReleaseYear = (int)tvSId.ReleaseYear,
                Seasons = (int)tvSId.Seasons
                // ! Not wanting to return a list, just one tv show detail
                // ! With the .ToListAsync(); it returns all TV shows
                // ! no matter what ID is entered
                // ! Switched it to .Where --> .FirstOrDefaultAsync(); instead
            })
            //.ToListAsync();
            .Where(
                tvSId => tvSId.Id == tvShowId
            )
            .FirstOrDefaultAsync();
            return tvShow;
        }

        // ! Removed IEnumerable from this since we are not returning a list
        public async Task<TVShowDetail> GetTVShowByTitleAsync(string tvShowTitle)
        {
            var tvShow = await _dbContext.TVShows.Select(tvSTitle => new TVShowDetail
            {
                Id = tvSTitle.Id,
                Title = tvSTitle.Title,
                ReleaseYear = (int)tvSTitle.ReleaseYear,
                Seasons = (int)tvSTitle.Seasons
                // ! Not wanting to return a list, just one tv show detail
                // ! With the .ToListAsync(); it returns all TV shows
                // ! no matter what ID is entered
                // ! Switched it to .Where --> .FirstOrDefaultAsync(); instead              
            })
            // .ToListAsync();
            .Where(
                tvSTitle => tvSTitle.Title == tvShowTitle
            )
            .FirstOrDefaultAsync();
            return tvShow;
        }

        public async Task<bool> UpdateTVShowAsync(int tvShowId, TVShowUpdate update)
        {
            var tvShowsFound = await _dbContext.TVShows.FindAsync(tvShowId);
            if (tvShowsFound is null)
            {
                return false;
            }
            var tvShowsUpdate = new TVShowsEntity
            {
                Title = update.Title
            };
            foreach (var tvS in await _dbContext.TVShows.ToListAsync())
            {
                if (ReformatUpdatedTitle(tvS) == ReformatUpdatedTitle(tvShowsUpdate))
                {
                    return false;
                }
            }
            tvShowsFound.Title = update.Title;
            tvShowsFound.ReleaseYear = update.ReleaseYear;
            tvShowsFound.Seasons = update.Seasons;
            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteTVShowAsync(int tvShowId)
        {
            var tvShowsDelete = await _dbContext.TVShows.FindAsync(tvShowId);
            _dbContext.TVShows.Remove(tvShowsDelete);
            return await _dbContext.SaveChangesAsync() == 1;
        }

        private string ReformatCreatedTitle(TVShowsEntity tvShowCreate)
        {
            var tvShowReform = String.Concat(tvShowCreate.Title.Split(' ', '-')).ToLower();
            return tvShowReform;
        }
        private string ReformatUpdatedTitle(TVShowsEntity tvShowUpdate)
        {
            var tvShowReform = String.Concat(tvShowUpdate.Title.Split(' ', '-')).ToLower();
            return tvShowReform;
        }
    }
}