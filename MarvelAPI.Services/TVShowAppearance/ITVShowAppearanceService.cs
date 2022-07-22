using MarvelAPI.Models.TVShowAppearance;

namespace MarvelAPI.Services.TVShowAppearance
{
    public interface ITVShowAppearanceService
    {
        Task<bool> CreateTVShowAppearanceAsync(TVShowAppearanceCreate model);
        Task<IEnumerable<TVShowAppearanceListItem>> GetAllTVShowAppearancesAsync();
        Task<TVShowAppearanceDetail> GetTVShowAppearanceByIdAsync(int tvShowAppearanceId);
        Task<bool> UpdateTVShowAppearanceAsync(int tvShowAppearanceId, TVShowAppearanceUpdate request);
        Task<bool> DeleteTVShowAppearanceAsync(int tvShowAppearanceId);
    }
}