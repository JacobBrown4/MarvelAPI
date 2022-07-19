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
        Task<MovieAppearanceDetail> GetMovieAppearanceDetailByIdAsync(int id);
        Task<bool> UpdateMovieAppearanceAsync(MovieAppearanceEntity request);
        Task<bool> DeleteMovieAppearanceAsync(int id);
    }
}