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

        public async Task<CharacterDetail> GetCharacterByIdAsync(int id)
        {
            var character = await _dbContext.Characters.FirstOrDefaultAsync(character => character.Id == id);
            var result = new CharacterDetail{
                Id = character.Id,
                FullName = character.FullName,
                Age = character.Age,
                Location = character.Location,
                Abilities = character.Abilities,
                AbilitiesOrigin = character.AbilitiesOrigin
            };
            return result;
        }

        public async Task<bool> UpdateCharacterAsync(CharacterEntity request)
        {
            var characterFound = await _dbContext.Characters.FindAsync(request.Id);
            if (characterFound is null) {
                return false;
            }
            characterFound.FullName = request.FullName;
            characterFound.Age = request.Age;
            characterFound.Location = request.Location;
            characterFound.Abilities = request.Abilities;
            characterFound.AbilitiesOrigin = request.AbilitiesOrigin;
            var numOfChanges = await _dbContext.SaveChangesAsync();
            return numOfChanges == 1;
        }
        
        public async Task<bool> DeleteCharacterAsync(int id)
        {
            var character = await _dbContext.Characters.FindAsync(id);
            _dbContext.Characters.Remove(character);
            return await _dbContext.SaveChangesAsync() == 1;
        }
    }
}