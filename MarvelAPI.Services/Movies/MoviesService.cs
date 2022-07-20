using MarvelAPI.Data;
using MarvelAPI.Data.Entities;
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

        public async Task<bool> CreateMoviesAsync (MoviesEntity request)
        {
            var moviesEntity = new MoviesEntity
            {
                Title = request.Title,
                ReleaseYear = request.ReleaseYear
            };
            _dbContext.Movies.Add(moviesEntity);
            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<MoviesEntity>> GetAllMoviesAsync()
        {
            var movies = await _dbContext.Movies
            .ToListAsync();
            return movies;
        }

        public async Task<bool> UpdateMoviesAsync(MoviesEntity request)
        {
            var moviesFound = await _dbContext.Movies.FindAsync(request.Id);
            if (moviesFound is null)
            {
                return false;
            }
            moviesFound.Title = request.Title;
            moviesFound.ReleaseYear = request.ReleaseYear;
            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteMoviesAsync(int Id)
        {
            var moviesDelete = await _dbContext.Movies.FindAsync(Id);
            _dbContext.Movies.Remove(moviesDelete);
            return await _dbContext.SaveChangesAsync() == 1;
        }
    }
}