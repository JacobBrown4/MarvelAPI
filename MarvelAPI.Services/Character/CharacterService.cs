using MarvelAPI.Data;
using MarvelAPI.Data.Entities;
using MarvelAPI.Models.Characters;
using Microsoft.EntityFrameworkCore;

namespace MarvelAPI.Services.Character
{
    public class CharacterService : ICharacterService
    {
        private readonly AppDbContext _dbContext;
        public CharacterService (AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateCharacterAsync(CharacterCreate model)
        {
            var character = new CharacterEntity{
                FullName = model.FullName,
                Age = model.Age
            };
            // Verify no duplicates by FullName (formatting for comparison)
            foreach (var c in await _dbContext.Characters.ToListAsync()) {
                if (ReformatFullName(c) == ReformatFullName(character)) {
                    return false;
                }
            }
            _dbContext.Characters.Add(character);
            var numOfChanges = await _dbContext.SaveChangesAsync();
            return numOfChanges == 1;
        }

        public async Task<IEnumerable<CharacterListItem>> GetAllCharactersAsync()
        {
            var characterList = await _dbContext.Characters.ToListAsync();
            var result = new List<CharacterListItem>();
            foreach ( var c in characterList) {
                result.Add(
                    new CharacterListItem{
                        Id = c.Id,
                        FullName = c.FullName
                    }
                );
            }
            return result;
        }

        public async Task<IEnumerable<CharacterAbilities>> GetCharactersByAbilityAsync(string ability) {
            var result = new List<CharacterAbilities>();
            // Getting most up-to-date list of characters
            var currentCharacters = await _dbContext.Characters.ToListAsync();
            foreach (var c in currentCharacters) {
                if (c.Abilities is null) {
                    // Skip characters with null Abilities
                    continue;
                }
                if (c.Abilities.ToLower().Contains(ability.ToLower())) {
                    result.Add(
                        new CharacterAbilities{
                            Id = c.Id,
                            FullName = c.FullName,
                            Abilities = c.Abilities
                        }
                    );
                }
            }
            return result;
        }

        public async Task<CharacterDetail> GetCharacterByIdAsync(int id)
        {
            var character = await _dbContext.Characters.FirstOrDefaultAsync(character => character.Id == id);
            var result = new CharacterDetail{
                Id = character.Id,
                FullName = character.FullName,
                Age = character.Age,
                Location = character.Location,
                Origin = character.Origin,
                Abilities = character.Abilities,
                AbilitiesOrigin = character.AbilitiesOrigin
            };
            return result;
        }

        public async Task<bool> UpdateCharacterAsync(int characterId, CharacterUpdate request)
        {
            var characterFound = await _dbContext.Characters.FindAsync(characterId);
            if (characterFound is null) {
                return false;
            }
            characterFound.FullName = CheckUpdateProperty(characterFound.FullName, request.FullName);
            characterFound.Age = CheckUpdateProperty(characterFound.Age, request.Age);
            characterFound.Location = CheckUpdateProperty(characterFound.Location, request.Location);
            characterFound.Origin = CheckUpdateProperty(characterFound.Origin, request.Origin);
            characterFound.Abilities = CheckUpdateProperty(characterFound.Abilities, request.Abilities);
            characterFound.AbilitiesOrigin = CheckUpdateProperty(characterFound.AbilitiesOrigin, request.AbilitiesOrigin);
            var numOfChanges = await _dbContext.SaveChangesAsync();
            return numOfChanges == 1;
        }
        
        public async Task<bool> DeleteCharacterAsync(int id)
        {
            var character = await _dbContext.Characters.FindAsync(id);
            _dbContext.Characters.Remove(character);
            return await _dbContext.SaveChangesAsync() == 1;
        }

        private string ReformatFullName(CharacterEntity character) {
            // Returns a reformatted version of a CharacterEntity's FullName,
            // to be more easily compared against others formatted the same way.

            // If there is a space/hyphen in the name, ignore it in the result to return
            var result = String.Concat(character.FullName.Split(' ', '-')).ToLower();
            return result;
        }

        private string CheckUpdateProperty(string from, string to) {
            return String.IsNullOrEmpty(to.Trim()) ? from : to.Trim();
        }
    }
}