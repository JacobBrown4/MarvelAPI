using System.Threading.Tasks;
using MarvelAPI.Data;
using MarvelAPI.Data.Entities;
using MarvelAPI.Models;
using MarvelAPI.Models.MovieAppearance;

namespace MarvelAPI.Services.MovieAppearance
{
    public interface IMovieAppearanceService
    {
        Task<bool> CreateMovieAppearanceAsync(MovieAppearanceCreate model);
        Task<IEnumerable<MovieAppearanceListItem>> GetAllMovieAppearancesAsync();
        Task<MovieAppearanceDetail> GetMovieAppearanceByIdAsync(int movieAppearanceId);
        // Task<IEnumerable<MoviesEntity>> GetMovieAppearanceByMovieIdAsync(int movieId);
        Task<bool> UpdateMovieAppearanceAsync(MovieAppearanceUpdate request);
        Task<bool> DeleteMovieAppearanceAsync(int movieAppearanceId);
    }
}