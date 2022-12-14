using System.Reflection;
using MarvelAPI.Data;
using MarvelAPI.Data.Entities;
using MarvelAPI.Models.Movies;
using MarvelAPI.Models.MovieAppearance;
using MarvelAPI.Models.Characters;
using Microsoft.EntityFrameworkCore;

namespace MarvelAPI.Services.MoviesService
{
    public class MovieService : IMovieService
    {
        private readonly AppDbContext _dbContext;
        public MovieService (AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateMoviesAsync (MovieCreate model)
        {
            var movie = new MoviesEntity
            {
                Title = model.Title,
                ReleaseYear = model.ReleaseYear
            };
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

        public async Task<IEnumerable<MovieListItem>> GetAllMoviesAsync()
        {
            var movieList = await _dbContext.Movies.Select(m => new MovieListItem
            {
                Id = m.Id,
                Title = m.Title
            }).ToListAsync();
            return movieList;
        }

        public async Task<MovieDetail> GetMovieByIdAsync(int movieId)
        {
            var movieFound = await _dbContext.Movies.Include(x=>x.Appearances).ThenInclude(y=>y.Character).FirstOrDefaultAsync(m=>m.Id == movieId);

            return new MovieDetail
            {
                Id = movieFound.Id,
                Title = movieFound.Title,
                ReleaseYear = (int)movieFound.ReleaseYear,
                Characters = movieFound.Appearances.Select(c=> new CharacterListItem{
                    Id = c.CharacterId,
                    FullName = c.Character.FullName
                }).ToList()
            };
        }

        public async Task<MovieDetail> GetMovieByTitleAsync(string movieTitle)
        {
            var movieFound = await _dbContext.Movies.Include(x=>x.Appearances).ThenInclude(y=>y.Character).FirstOrDefaultAsync(m=>m.Title.ToLower().Contains(movieTitle.ToLower()));

            return new MovieDetail
            {
                Id = movieFound.Id,
                Title = movieFound.Title,
                ReleaseYear = (int)movieFound.ReleaseYear,
                Characters = movieFound.Appearances.Select(c=> new CharacterListItem{
                    Id = c.CharacterId,
                    FullName = c.Character.FullName
                }).ToList()
            };
        }

        public async Task<bool> UpdateMoviesAsync(int movieId, MovieUpdate request)
        {
            var movieFound = await _dbContext.Movies.FindAsync(movieId);
            if (movieFound is null)
            {
                return false;
            }
            var movieUpdate = new MoviesEntity
            {
                Title = request.Title
            };
            foreach (var m in await _dbContext.Movies.ToListAsync())
            {
                if (ReformatTitle(m) == ReformatTitle(movieUpdate))
                {
                    return false;
                }
            }
            movieFound.Title = request.Title;
            movieFound.ReleaseYear = request.ReleaseYear;
            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteMoviesAsync(int movieId)
        {
            var moviesDelete = await _dbContext.Movies.FindAsync(movieId);
            if (moviesDelete is null) 
            {
                return false;
            }
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