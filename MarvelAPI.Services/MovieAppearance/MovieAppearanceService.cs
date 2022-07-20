using System;
using System.Threading.Tasks;
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
            var character = await _dbContext.Characters.FindAsync(model.CharacterId);
            var movie = await _dbContext.Movies.FindAsync(model.MovieId);
            var movieAppearance = new MovieAppearanceEntity
            {
                CharacterId = model.CharacterId,
                MovieId = model.MovieId,
                Movie = movie,
                Character = character

            };

            _dbContext.MovieAppearances.Add(movieAppearance);

            var numberOfChanges = await _dbContext.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        // * GET
        public async Task<IEnumerable<MovieAppearanceListItem>> GetAllMovieAppearancesAsync()
        {
            var movieAppearanceList = await _dbContext.MovieAppearances.ToListAsync();
            // return movieAppearanceList;
            var result = new List<MovieAppearanceListItem>();
            foreach (var mA in movieAppearanceList) 
            {
                if (mA.Character is null || mA.Movie is null)
                {
                    continue;
                }

                result.Add(new MovieAppearanceListItem
                {
                    Id = mA.Id,
                    Movie = mA.Movie.Title,
                    Character = mA.Character.FullName

                });
            }
            return result;
        }

        public async Task<MovieAppearanceDetail> GetMovieAppearanceByIdAsync (int movieAppearanceId)
        {
            var movieAppearance = await _dbContext.MovieAppearances.FirstOrDefaultAsync(mA => mA.Id == movieAppearanceId);

            return movieAppearance is null ? null : new MovieAppearanceDetail
            {
                Id = movieAppearance.Id,
                MovieId = movieAppearance.MovieId,
                CharacterId = movieAppearance.CharacterId
            };
        }

        // * PUT
        public async Task<bool> UpdateMovieAppearanceAsync(MovieAppearanceUpdate request)
        {
            var movieAppearanceFound = await _dbContext.MovieAppearances.FindAsync(request.Id);

            if (movieAppearanceFound is null)
            {
                return false;
            }
            movieAppearanceFound.CharacterId = request.CharacterId;
            movieAppearanceFound.MovieId = request.MovieId;

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