using MarvelAPI.Data;
using MarvelAPI.Data.Entities;
using MarvelAPI.Models.Movies;
using Microsoft.EntityFrameworkCore;

namespace MarvelAPI.Services.MoviesService
{
    public class MoviesService : IMoviesService
    {
        private readonly AppDbContext _dbContext;
        public MoviesService (AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateMoviesAsync (MoviesCreate model)
        {
            var movie = new MoviesEntity
            {
                Title = model.Title,
                ReleaseYear = model.ReleaseYear
            };
            // Verify no duplicates by Title (formatting for comparison)
            foreach (var m in await _dbContext.Movies.ToListAsync())
            {
                if (ReformatTitle(m) == ReformatTitle(movie)) 
                {
                    return false;
                }
            }
            _dbContext.Movies.Add(movie);
            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<MoviesListItem>> GetAllMoviesAsync()
        {
            var movieList = await _dbContext.Movies.Select(m => new MoviesListItem
            {
                Id = m.Id,
                Title = m.Title
            }).ToListAsync();
            return movieList;
        }

        public async Task<IEnumerable<MoviesDetail>> GetMovieByIdAsync(int moviesId)
        {
            var movieId = await _dbContext.Movies.Select(mL => new MoviesDetail
            {
                Id = mL.Id,
                Title = mL.Title,
                ReleaseYear = (int)mL.ReleaseYear
            }).ToListAsync();
            return movieId;
        }

        public async Task<IEnumerable<MoviesDetail>> GetMovieByTitleAsync(string moviesTitle)
        {
            var movieTitle = await _dbContext.Movies.Select(mT => new MoviesDetail
            {
                Id = mT.Id,
                Title = mT.Title,
                ReleaseYear = (int)mT.ReleaseYear
            }).ToListAsync();
            return movieTitle;
        }

        public async Task<bool> UpdateMoviesAsync(int moviesId, MoviesUpdate update)
        {
            var moviesFound = await _dbContext.Movies.FindAsync(moviesId);
            if (moviesFound is null)
            {
                return false;
            }
            var moviesUpdate = new MoviesEntity
            {
                Title = update.Title
            };
            foreach (var m in await _dbContext.Movies.ToListAsync())
            {
                if (ReformatTitle(m) == ReformatTitle(moviesUpdate))
                {
                    return false;
                }
            }
            moviesFound.Title = update.Title;
            moviesFound.ReleaseYear = update.ReleaseYear;
            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteMoviesAsync(int moviesId)
        {
            var moviesDelete = await _dbContext.Movies.FindAsync(moviesId);
            _dbContext.Movies.Remove(moviesDelete);
            return await _dbContext.SaveChangesAsync() == 1;
        }

        private string ReformatTitle(MoviesEntity movie) 
        {
            var result = String.Concat(movie.Title.Split(' ', '-')).ToLower();
            return result;
        }
        private string CheckUpdateProperty(string from, string to) {
            return String.IsNullOrEmpty(to.Trim()) ? from : to.Trim();
        }
    }
}