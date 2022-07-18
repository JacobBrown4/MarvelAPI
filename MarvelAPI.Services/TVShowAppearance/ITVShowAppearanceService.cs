using MarvelAPI.Data.Entities;
using MarvelAPI.Models;
using MarvelAPI.Models.TVShowAppearance;

namespace MarvelAPI.Services.TVShowAppearance
{
    public interface ITVShowAppearanceService
    {
        Task<bool> CreateTVShowAppearanceAsync(TVShowAppearanceCreate request);
        Task<IEnumerable<TVShowAppearanceEntity>> GetAllTVShowAppearanceAsync();
        Task<TVShowAppearanceDetail> GetTVShowAppearanceDetailByIdAsync(int id);
        Task<bool> UpdateTVShowAppearanceAsync(TVShowAppearanceDetail request);
        Task<bool> DeleteTVShowAppearanceAsync(int id);
    }
}