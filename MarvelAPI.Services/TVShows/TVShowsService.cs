using MarvelAPI.Data;
using MarvelAPI.Data.Entities;
using MarvelAPI.Models.TVShows;
using MarvelAPI.Models.TVShowAppearance;
using MarvelAPI.Models.Characters;
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

        public async Task<TVShowDetail> GetTVShowByIdAsync(int tvShowId)
        {
            var tvShow = await _dbContext.TVShows.FindAsync(tvShowId);
            
            var tvShowChar = await _dbContext.TVShowAppearance
            .Select(
                tvSA => new TVShowAppearanceDetail
            {
                Id = tvSA.Id,
                CharacterId = tvSA.Character.Id,
                Character = tvSA.Character.FullName,
                TVShowId = tvSA.TVShow.Id,
                TVShow = tvSA.TVShow.Title
            })
            .Where(
                tvS => tvS.TVShow == tvShow.Title
            )
            .Select(
                cLI => new CharacterListItem
                {
                    Id = cLI.CharacterId,
                    FullName = cLI.Character
                }
            )
            .ToListAsync();
            var tvSID = new TVShowDetail
            {
                Id = tvShow.Id,
                Title = tvShow.Title,
                ReleaseYear = (int)tvShow.ReleaseYear,
                Seasons = (int)tvShow.Seasons,
                Characters = tvShowChar
            };
            return tvSID;
        }

        public async Task<TVShowDetail> GetTVShowByTitleAsync(string tvShowTitle)
        {
            var tvShow = await _dbContext.TVShows
            .Select(
                tvSTitle => new TVShowDetail
            {
                Id = tvSTitle.Id,
                Title = tvSTitle.Title,
                ReleaseYear = (int)tvSTitle.ReleaseYear,
                Seasons = (int)tvSTitle.Seasons           
            })
            .Where(
                tvSTitle => tvSTitle.Title == tvShowTitle
            )
            .FirstOrDefaultAsync();
            var tvShowChar = await _dbContext.TVShowAppearance
            .Select(
                tvSA => new TVShowAppearanceDetail
                {
                    Id = tvSA.Id,
                    CharacterId = tvSA.Character.Id,
                    Character = tvSA.Character.FullName,
                    TVShowId = tvSA.TVShow.Id,
                    TVShow = tvSA.TVShow.Title
                }
            )
            .Where(
                tvS => tvS.TVShow == tvShow.Title
            )
            .Select(
                cLI => new CharacterListItem
                {
                    Id = cLI.CharacterId,
                    FullName = cLI.Character
                }
            )
            .ToListAsync();
            var tvSTitle = new TVShowDetail
            {
                Id = tvShow.Id,
                Title = tvShow.Title,
                ReleaseYear = (int)tvShow.ReleaseYear,
                Seasons = (int)tvShow.Seasons,
                Characters = tvShowChar
            };
            return tvSTitle;
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
            if (tvShowDelete is null) 
            {
                return false;
            }
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