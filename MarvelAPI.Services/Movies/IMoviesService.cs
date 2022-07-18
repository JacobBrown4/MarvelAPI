using MarvelAPI.Data.Entities;

namespace MarvelAPI.Services.MoviesService
{
    public interface IMoviesService
    {
        Task<bool> CreateMoviesAsync(MoviesEntity request);
        Task<IEnumerable<MoviesEntity>> GetAllMoviesAsync();
        Task<bool> UpdateMoviesAsync(MoviesEntity request);
        Task<bool> DeleteMoviesAsync(int id);
    }
}