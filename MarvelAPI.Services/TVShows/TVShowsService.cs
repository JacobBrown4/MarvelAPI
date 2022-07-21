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

        public async Task<IEnumerable<TVShowsDetail>> GetTVShowsByTitleAsync(string Title)
        {
            var tvShow = new List<TVShowsDetail>();
            var tvShowTitle = await _dbContext.TVShows.ToListAsync();
            foreach (var tvS in tvShowTitle)
            {
                if (tvS.Title is null)
                {
                    continue;
                }
                if (tvS.Title.ToLower().Contains(Title.ToLower()))
                {
                    tvShow.Add(new TVShowsDetail
                    {
                        Id = tvS.Id,
                        Title = tvS.Title,
                        ReleaseYear = (int)tvS.ReleaseYear,
                        Seasons = (int)tvS.Seasons
                    });
                }
            }
            return tvShow;
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