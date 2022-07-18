using MarvelAPI.Data.Entities;
using MarvelAPI.Models.Characters;

namespace MarvelAPI.Services.Character
{
    public interface ICharacterService
    {
        Task<bool> CreateCharacterAsync(CharacterCreate model);
        Task<IEnumerable<CharacterListItem>> GetAllCharactersAsync();
        Task<CharacterDetail> GetCharacterById(int id);
        Task<bool> UpdateCharacterAsync(CharacterEntity request);
        Task<bool> DeleteCharacterAsync(int id);
    }
}