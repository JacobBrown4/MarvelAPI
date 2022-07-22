using MarvelAPI.Data;
using MarvelAPI.Data.Entities;
using MarvelAPI.Models.MovieAppearance;
using Microsoft.EntityFrameworkCore;

namespace MarvelAPI.Services.MovieAppearance
{
    public class MovieAppearanceService : IMovieAppearanceService
    {
        private readonly AppDbContext _dbContext;
        public MovieAppearanceService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // * POST
        public async Task<bool> CreateMovieAppearanceAsync(MovieAppearanceCreate request)
        {
            var entity = new MovieAppearanceEntity
            {
                CharacterId = request.CharacterId,
                MovieId = request.MovieId
            };

            _dbContext.MovieAppearances.Add(entity);

            var numberOfChanges = await _dbContext.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        // * GET
        public async Task<IEnumerable<MovieAppearanceEntity>> GetAllMovieAppearancesAsync()
        {
            var movieAppearances = await _dbContext.MovieAppearances

            .ToListAsync();

            return movieAppearances;
        }

        public async Task<MovieAppearanceDetail> GetMovieAppearanceDetailByIdAsync (int id)
        {
            var movieAppearanceEntity = await _dbContext.MovieAppearances.FirstOrDefaultAsync(e => e.Id == id);

            return movieAppearanceEntity is null ? null : new MovieAppearanceDetail
            {
                Id = movieAppearanceEntity.Id,
                MovieId = movieAppearanceEntity.MovieId,
                CharacterId = movieAppearanceEntity.CharacterId
            };
        }
        // Add get by name?
        // * PUT
        public async Task<bool> UpdateMovieAppearanceAsync(MovieAppearanceDetail request)
        {
            var movieAppearanceEntity = await _dbContext.MovieAppearances.FindAsync(request.Id);
            movieAppearanceEntity.CharacterId = request.CharacterId;
            movieAppearanceEntity.MovieId = request.MovieId;

            var numberOfChanges = await _dbContext.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        // * DELETE
        public async Task<bool> DeleteMovieAppearanceAsync(int id)
        {
            var movieAppearanceEntity = await _dbContext.MovieAppearances.FindAsync(id);

            _dbContext.MovieAppearances.Remove(movieAppearanceEntity);

            return await _dbContext.SaveChangesAsync() == 1;
        }
    }
}