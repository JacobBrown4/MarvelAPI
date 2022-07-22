using MarvelAPI.Data;
using MarvelAPI.Data.Entities;
using MarvelAPI.Models.TVShows;
using Microsoft.EntityFrameworkCore;

namespace MarvelAPI.Services.TVShowsService
{
    public class TVShowService : ITVShowService
    {
        private readonly AppDbContext _dbContext;
        public TVShowService (AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateTVShowAsync (TVShowCreate model)
        {
            var tvShowCreate = new TVShowsEntity
            {
                Title = model.Title,
                ReleaseYear = model.ReleaseYear,
                Seasons = model.Seasons
            };
            foreach (var tvS in await _dbContext.TVShows.ToListAsync())
            {
                if (ReformatTitle(tvS) == ReformatTitle(tvShowCreate))
                {
                    return false;
                }
            }
            _dbContext.TVShows.Add(tvShowCreate);
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

        public async Task<bool> UpdateTVShowAsync(int tvShowId, TVShowUpdate request)
        {
            var tvShowFound = await _dbContext.TVShows.FindAsync(tvShowId);
            if (tvShowFound is null)
            {
                return false;
            }
            var tvShowUpdate = new TVShowsEntity
            {
                Title = request.Title
            };
            foreach (var tvS in await _dbContext.TVShows.ToListAsync())
            {
                if (ReformatTitle(tvS) == ReformatTitle(tvShowUpdate))
                {
                    return false;
                }
            }
            tvShowFound.Title = request.Title;
            tvShowFound.ReleaseYear = request.ReleaseYear;
            tvShowFound.Seasons = request.Seasons;
            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteTVShowAsync(int tvShowId)
        {
            var tvShowDelete = await _dbContext.TVShows.FindAsync(tvShowId);
            _dbContext.TVShows.Remove(tvShowDelete);
            return await _dbContext.SaveChangesAsync() == 1;
        }

        private string ReformatTitle(TVShowsEntity tvShow)
        {
            var tvShowReform = String.Concat(tvShow.Title.Split(' ', '-')).ToLower();
            return tvShowReform;
        }
    }
}