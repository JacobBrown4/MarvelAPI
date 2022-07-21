using MarvelAPI.Data.Entities;
using MarvelAPI.Models.TVShows;

namespace MarvelAPI.Services.TVShowsService
{
    public interface ITVShowsService
    {
        Task<bool> CreateTVShowsAsync(TVShowsCreate model);
        Task<IEnumerable<TVShowsListItem>> GetAllTVShowsAsync();
        Task<IEnumerable<TVShowsDetail>> GetTVShowsByIdAsync(int Id);
        Task<IEnumerable<TVShowsDetail>> GetTVShowsByTitleAsync(string Title);
        Task<bool> UpdateTVShowsAsync(int tvShowId, TVShowsUpdate update);
        Task<bool> DeleteTVShowsAsync(int id);
    }
}