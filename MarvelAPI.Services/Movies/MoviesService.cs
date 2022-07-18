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

        public async Task<bool> CreateMoviesAsync (Movies request)
        {
            var moviesEntity = new Movies
            {
                Title = request.Title,
                ReleaseYear = request.ReleaseYear
            };
            _dbContext.Movies.Add(moviesEntity);
            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<Movies>> GetAllMoviesAsync()
        {
            var movies = await _dbContext.Movies
            .ToListAsync();
            return movies;
        }

        public async Task<bool> UpdateMoviesAsync(Movies request)
        {
            var moviesEntity = await _dbContext.Movies.FindAsync(request.Id);
            moviesEntity.Title = request.Title;
            moviesEntity.ReleaseYear = request.ReleaseYear;
            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteMoviesAsync(int Id)
        {
            var moviesEntity = await _dbContext.Movies.FindAsync(Id);
            _dbContext.Movies.Remove(moviesEntity);
            return await _dbContext.SaveChangesAsync() == 1;
        }
    }
}