using MarvelAPI.Data.Entities;
using MarvelAPI.Models.TVShows;

namespace MarvelAPI.Services.TVShowsService
{
    public interface ITVShowsService
    {
        Task<bool> CreateTVShowsAsync(TVShowsEntity request);
        Task<IEnumerable<TVShowsListItem>> GetAllTVShowsAsync();
        Task<TVShowsDetail> GetTVShowsByIdAsync(int Id);
        Task<bool> UpdateTVShowsAsync(TVShowsUpdate request);
        Task<bool> DeleteTVShowsAsync(int id);
    }
}