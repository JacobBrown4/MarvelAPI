using MarvelAPI.Models.MovieAppearance;

namespace MarvelAPI.Services.MovieAppearance
{
    public interface IMovieAppearanceService
    {
        Task<bool> CreateMovieAppearanceAsync(MovieAppearanceCreate model);
        Task<IEnumerable<MovieAppearanceListItem>> GetAllMovieAppearancesAsync();
        Task<MovieAppearanceDetail> GetMovieAppearanceByIdAsync(int movieAppearanceId);
        Task<bool> UpdateMovieAppearanceAsync(int movieAppearanceId, MovieAppearanceUpdate request);
        Task<bool> DeleteMovieAppearanceAsync(int movieAppearanceId);
    }
}