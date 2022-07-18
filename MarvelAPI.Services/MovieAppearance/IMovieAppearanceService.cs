using System.Threading.Tasks;
using MarvelAPI.Data;
using MarvelAPI.Data.Entities;
using MarvelAPI.Models;
using MarvelAPI.Models.MovieAppearance;

namespace MarvelAPI.Services.MovieAppearance
{
    public interface IMovieAppearanceService
    {
        Task<bool> CreateMovieAppearanceAsync(MovieAppearanceCreate request);
        Task<IEnumerable<MovieAppearanceEntity>> GetAllMovieAppearancesAsync();
        Task<MovieAppearanceDetail> GetMovieAppearanceDetailByIdAsync(int id);
        Task<bool> UpdateMovieAppearanceAsync(MovieAppearanceDetail request);
        Task<bool> DeleteMovieAppearanceAsync(int id);
    }
}