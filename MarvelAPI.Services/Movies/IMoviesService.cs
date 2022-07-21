using MarvelAPI.Data.Entities;
using MarvelAPI.Models.Movies;

namespace MarvelAPI.Services.MoviesService
{
    public interface IMoviesService
    {
        Task<bool> CreateMoviesAsync(MoviesCreate model);
        Task<IEnumerable<MoviesListItem>> GetAllMoviesAsync();
        Task<IEnumerable<MoviesDetail>> GetMovieByIdAsync(int moviesId);
        Task<IEnumerable<MoviesDetail>> GetMovieByTitleAsync(string moviesTitle);
        Task<bool> UpdateMoviesAsync(int moviesId, MoviesUpdate request);
        Task<bool> DeleteMoviesAsync(int moviesId);
    }
}