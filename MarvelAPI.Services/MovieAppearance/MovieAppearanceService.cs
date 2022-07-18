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
        public async Task<bool> CreateMovieAppearanceAsync(MovieAppearanceCreate model)
        {
            var entity = new MovieAppearanceEntity
            {
                CharacterId = model.CharacterId,
                MovieId = model.MovieId
            };

            _dbContext.MovieAppearances.Add(entity);

            var numberOfChanges = await _dbContext.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        private async Task<MovieAppearanceEntity> GetMovieAppearanceByIdAsync(int id)
        {
            return await _dbContext.MovieAppearances.FirstOrDefaultAsync(movieAppearance => movieAppearance.Id == id);
        }
    }
}