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
        public async Task<bool> CreateMovieAppearanceAsync(MovieAppearanceCreate model)
        {
            var movieAppearance = new MovieAppearanceEntity
            {
                CharacterId = model.CharacterId,
                MovieId = model.MovieId,
            };
            
            _dbContext.MovieAppearances.Add(movieAppearance);
            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        // * GET
        public async Task<IEnumerable<MovieAppearanceListItem>> GetAllMovieAppearancesAsync()
        {
            var movieAppearanceList = await _dbContext.MovieAppearances
            .Select(
                x => new MovieAppearanceListItem
                {
                    Id = x.Id,
                    Character = x.Character.FullName,
                    Movie = x.Movie.Title,
                }
            )
            .ToListAsync();
            return movieAppearanceList;
        }

        // * GET
        public async Task<MovieAppearanceDetail> GetMovieAppearanceByIdAsync(int movieAppearanceId)
        {
            var movieAppearance = await _dbContext.MovieAppearances
            .Select(
                x => new MovieAppearanceDetail
                {
                    Id = x.Id,
                    CharacterId = x.CharacterId,
                    Character = x.Character.FullName,
                    MovieId = x.MovieId,
                    Movie = x.Movie.Title
                }
            )
            .Where(
                x => x.Id == movieAppearanceId
            )
            .FirstOrDefaultAsync();
            return movieAppearance;
        }

        // * PUT
        public async Task<bool> UpdateMovieAppearanceAsync(int movieAppearanceId, MovieAppearanceUpdate request)
        {
            var movieAppearance = await _dbContext.MovieAppearances.FindAsync(movieAppearanceId);

            if (movieAppearance is null)
            {
                return false;
            }
            movieAppearance.CharacterId = request.CharacterId;
            movieAppearance.MovieId = request.MovieId;

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        // * DELETE
        public async Task<bool> DeleteMovieAppearanceAsync(int movieAppearanceId)
        {
            var movieAppearance = await _dbContext.MovieAppearances.FindAsync(movieAppearanceId);
            _dbContext.MovieAppearances.Remove(movieAppearance);
            return await _dbContext.SaveChangesAsync() == 1;
        }
    }
}