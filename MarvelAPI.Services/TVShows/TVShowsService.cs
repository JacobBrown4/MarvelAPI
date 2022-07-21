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

        public async Task<bool> CreateTVShowsAsync (TVShowsCreate model)
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

        public async Task<IEnumerable<TVShowsListItem>> GetAllTVShowsAsync()
        {
            var tvShowList = await _dbContext.TVShows.Select(tvS => new TVShowsListItem
            {
                Id = tvS.Id,
                Title = tvS.Title
            }).ToListAsync();
            return tvShowList;
        }
        public async Task<IEnumerable<TVShowsDetail>> GetTVShowsByIdAsync(int Id)
        {
            var tvShowId = await _dbContext.TVShows.Select(tvSId => new TVShowsDetail
            {
                Id = tvSId.Id,
                Title = tvSId.Title,
                ReleaseYear = (int)tvSId.ReleaseYear,
                Seasons = (int)tvSId.Seasons
            }).ToListAsync();
            return tvShowId;
        }

        public async Task<IEnumerable<TVShowsDetail>> GetTVShowsByTitleAsync(string Title)
        {
            var tvShowTitle = await _dbContext.TVShows.Select(tvSTitle => new TVShowsDetail
            {
                Id = tvSTitle.Id,
                Title = tvSTitle.Title,
                ReleaseYear = (int)tvSTitle.ReleaseYear,
                Seasons = (int)tvSTitle.Seasons                
            }).ToListAsync();
            return tvShowTitle;
        }

        public async Task<bool> UpdateTVShowsAsync(int tvShowsId, TVShowsUpdate update)
        {
            var tvShowsFound = await _dbContext.TVShows.FindAsync(tvShowsId);
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

        public async Task<bool> DeleteTVShowsAsync(int Id)
        {
            var tvShowsDelete = await _dbContext.TVShows.FindAsync(Id);
            _dbContext.TVShows.Remove(tvShowsDelete);
            return await _dbContext.SaveChangesAsync() == 1;
        }

        private string ReformatCreatedTitle(TVShowsEntity tvShowsCreate)
        {
            var tvShowReform = String.Concat(tvShowsCreate.Title.Split(' ', '-')).ToLower();
            return tvShowReform;
        }
        private string ReformatUpdatedTitle(TVShowsEntity tvShowsUpdate)
        {
            var tvShowReform = String.Concat(tvShowsUpdate.Title.Split(' ', '-')).ToLower();
            return tvShowReform;
        }
    }
}