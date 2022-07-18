using MarvelAPI.Data.Entities;

namespace MarvelAPI.Services.MoviesService
{
    public interface IMoviesService
    {
        Task<bool> CreateMoviesAsync(Movies request);
        Task<IEnumerable<Movies>> GetAllMoviesAsync();
        Task<bool> UpdateMoviesAsync(Movies request);
        Task<bool> DeleteMoviesAsync(int id);
    }
}