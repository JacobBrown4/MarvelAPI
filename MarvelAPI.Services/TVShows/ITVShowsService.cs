using MarvelAPI.Data;
using MarvelAPI.Data.Entities;

namespace MarvelAPI.Services.TVShowsService
{
    public interface ITVShowsService
    {
        Task<bool> CreateTVShowsAsync(TVShowsEntity request);
        Task<IEnumerable<TVShowsEntity>> GetAllTVShowsAsync();
        Task<bool> UpdateTVShowsAsync(TVShowsEntity request);
        Task<bool> DeleteTVShowsAsync(int id);
    }
}