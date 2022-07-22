using MarvelAPI.Data.Entities;
using MarvelAPI.Models.Movies;

namespace MarvelAPI.Services.MoviesService
{
    public interface IMovieService
    {
        Task<bool> CreateMoviesAsync(MovieCreate model);
        Task<IEnumerable<MovieListItem>> GetAllMoviesAsync();
        Task<MovieDetail> GetMovieByIdAsync(int movieId);
        Task<MovieDetail> GetMovieByTitleAsync(string movieTitle);
        Task<bool> UpdateMoviesAsync(int movieId, MovieUpdate request);
        Task<bool> DeleteMoviesAsync(int movieId);
    }
}