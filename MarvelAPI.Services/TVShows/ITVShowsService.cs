using MarvelAPI.Data.Entities;
using MarvelAPI.Models.TVShows;

namespace MarvelAPI.Services.TVShowsService
{
    public interface ITVShowsService
    {
        Task<bool> CreateTVShowAsync(TVShowCreate model);
        Task<IEnumerable<TVShowListItem>> GetAllTVShowsAsync();
        Task<TVShowDetail> GetTVShowByIdAsync(int tvShowId);
        Task<TVShowDetail> GetTVShowByTitleAsync(string tvShowTitle);
        Task<bool> UpdateTVShowAsync(int tvShowId, TVShowUpdate request);
        Task<bool> DeleteTVShowAsync(int tvShowId);
    }
}