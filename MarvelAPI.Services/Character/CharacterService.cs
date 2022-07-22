using MarvelAPI.Data;
using MarvelAPI.Data.Entities;
using MarvelAPI.Models.Characters;
using MarvelAPI.Models.MovieAppearance;
using MarvelAPI.Models.Movies;
using MarvelAPI.Models.TVShowAppearance;
using MarvelAPI.Models.TVShows;
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
            var result = await _dbContext.Characters
                .Select(
                    c => new CharacterListItem{
                        Id = c.Id,
                        FullName = c.FullName
                })
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<CharacterAbilities>> GetCharactersByAbilityAsync(string ability) {
            var result = await _dbContext.Characters
                .Select(
                    c => new CharacterAbilities{
                        Id = c.Id,
                        FullName = c.FullName,
                        Abilities = c.Abilities
                })
                .Where(
                    o => o.Abilities != null && 
                    o.Abilities.ToLower().Contains(ability.ToLower())
                )
                .ToListAsync();
            return result;
        }

        public async Task<CharacterDetail> GetCharacterByIdAsync(int id)
        {
            var characterFound = await _dbContext.Characters.FindAsync(id);
            var characterMovies = await _dbContext.MovieAppearances
                .Select(
                    ma => new MovieAppearanceDetail{
                        Id = ma.Id,
                        Character = ma.Character.FullName,
                        CharacterId = ma.CharacterId,
                        Movie = ma.Movie.Title,
                        MovieId = ma.MovieId
                })
                .Where(
                    o => o.Character == characterFound.FullName
                )
                .Select(
                    mad => new MovieListItem{
                        Id = mad.MovieId,
                        Title = mad.Movie
                    }
                )
                .ToListAsync();
            
            var characterTVShows = await _dbContext.TVShowAppearance
                .Select(
                    tvsa => new TVShowAppearanceDetail{
                        Id = tvsa.Id,
                        TVShowId = tvsa.TVShowId,
                        CharacterId = tvsa.CharacterId,
                        TVShow = tvsa.TVShow.Title,
                        Character = tvsa.Character.FullName
                })
                .Where(
                    o => o.CharacterId == characterFound.Id
                )
                .Select(
                    tvsad => new TVShowListItem{
                        Id = tvsad.TVShowId,
                        Title = tvsad.TVShow
                    }
                )
                .ToListAsync();
            
            var result = new CharacterDetail{
                Id = characterFound.Id,
                FullName = characterFound.FullName,
                Age = characterFound.Age,
                Location = characterFound.Location,
                Origin = characterFound.Origin,
                Abilities = characterFound.Abilities,
                AbilitiesOrigin = characterFound.AbilitiesOrigin,
                Aliases = characterFound.Aliases,
                Status = characterFound.Status,
                Movies = characterMovies,
                TVShows = characterTVShows
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
            characterFound.Aliases = CheckUpdateProperty(characterFound.Aliases, request.Aliases);
            characterFound.Status = CheckUpdateProperty(characterFound.Status, request.Status);
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