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
            var movieAppearance = new MovieAppearanceEntity
            {
                CharacterId = model.CharacterId,
                MovieId = model.MovieId
            };

            _dbContext.MovieAppearances.Add(movieAppearance);

            var numberOfChanges = await _dbContext.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        // * GET
        public async Task<IEnumerable<MovieAppearanceListItem>> GetAllMovieAppearancesAsync()
        {
            var movieAppearanceList = await _dbContext.MovieAppearances.ToListAsync();
            var result = new List<MovieAppearanceListItem>();
            foreach (var mA in movieAppearanceList) 
            {
                result.Add(new MovieAppearanceListItem
                {
                    Id = mA.Id,
                    Movie = mA.Movie,
                    Character = mA.Character

                });
            }
            return result;
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