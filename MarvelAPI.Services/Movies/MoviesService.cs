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
            var movieList = await _dbContext.Movies.ToListAsync();
            var result = new List<MoviesListItem>();
            foreach (var m in movieList)
            {
                result.Add
                (
                    new MoviesListItem
                    {
                        Id = m.Id,
                        Title = m.Title
                    }
                );
            }
            return result;
        }

        public async Task<MoviesDetail> GetMovieByIdAsync(int moviesId)
        {
            var movie = await _dbContext.Movies.FirstOrDefaultAsync(movie => movie.Id == moviesId);
            var result = new MoviesDetail
            {
                Id = movie.Id,
                Title = movie.Title,
                ReleaseYear = (int)movie.ReleaseYear
            };
            return result;
        }

        public async Task<IEnumerable<MoviesDetail>> GetMovieByTitleAsync(string movieTitle)
        {
            var result = new List<MoviesDetail>();
            var currentMovie = await _dbContext.Movies.ToListAsync();
            foreach (var m in currentMovie)
            {
                if (m.Title is null)
                {
                    continue;
                }
                if (m.Title.ToLower().Contains(movieTitle.ToLower()))
                {
                    result.Add
                    (
                        new MoviesDetail
                        {
                            Id = m.Id,
                            Title = m.Title,
                            ReleaseYear = (int)m.ReleaseYear
                        }
                    );
                }
            }
            return result;
        }

        public async Task<bool> UpdateMoviesAsync(int moviesId, MoviesUpdate request)
        {
            var moviesFound = await _dbContext.Movies.FindAsync(moviesId);
            if (moviesFound is null)
            {
                return false;
            }

            var movie = new MoviesEntity
            {
                Title = request.Title
            };

            foreach (var m in await _dbContext.Movies.ToListAsync())
            {
                if (ReformatTitle(m) == ReformatTitle(movie))
                {
                    return false;
                }
            }
            moviesFound.Title = request.Title;
            moviesFound.ReleaseYear = request.ReleaseYear;
            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteMoviesAsync(int moviesId)
        {
            var moviesDelete = await _dbContext.Movies.FindAsync(moviesId);
            _dbContext.Movies.Remove(moviesDelete);
            return await _dbContext.SaveChangesAsync() == 1;
        }

        private string ReformatTitle(MoviesEntity movie) {
            // Returns a reformatted version of a MoviesEntity's Title,
            // to be more easily compared against others formatted the same way.

            // If there is a space/hyphen in the name, ignore it in the result to return
            var result = String.Concat(movie.Title.Split(' ', '-')).ToLower();
            return result;
        }
    }
}