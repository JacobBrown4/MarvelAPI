using MarvelAPI.Data.Entities;
using MarvelAPI.Models.Movies;

namespace MarvelAPI.Services.MoviesService
{
    public interface IMoviesService
    {
        Task<bool> CreateMoviesAsync(MoviesCreate model);
        Task<IEnumerable<MoviesListItem>> GetAllMoviesAsync();
        Task<MoviesDetail> GetMovieByIdAsync(int moviesId);
        Task<IEnumerable<MoviesDetail>> GetMovieByTitleAsync(string movieTitle);
        Task<bool> UpdateMoviesAsync(int moviesId, MoviesUpdate request);
        Task<bool> DeleteMoviesAsync(int moviesId);
    }
}