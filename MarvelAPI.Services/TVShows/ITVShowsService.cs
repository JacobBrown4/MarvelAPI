using MarvelAPI.Data;
using MarvelAPI.Data.Entities;

namespace MarvelAPI.Services.TVShowsService
{
    public interface ITVShowsService
    {
        Task<bool> CreateTVShowsAsync(TVShows request);
        Task<IEnumerable<TVShows>> GetAllTVShowsAsync();
        Task<bool> UpdateTVShowsAsync(TVShows request);
        Task<bool> DeleteTVShowsAsync(int id);
    }
}