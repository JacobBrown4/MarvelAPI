using MarvelAPI.Data.Entities;
using MarvelAPI.Models.Characters;

namespace MarvelAPI.Services.Character
{
    public interface ICharacterService
    {
        Task<bool> CreateCharacterAsync(CharacterCreate model);
        Task<IEnumerable<CharacterListItem>> GetAllCharactersAsync();
        Task<IEnumerable<CharacterAbilities>> GetCharactersByAbilityAsync(string ability);
        Task<CharacterDetail> GetCharacterByIdAsync(int id);
        Task<bool> UpdateCharacterAsync(CharacterEntity request);
        Task<bool> DeleteCharacterAsync(int id);
    }
}