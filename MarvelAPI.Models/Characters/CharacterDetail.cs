using MarvelAPI.Models.Movies;
using MarvelAPI.Models.TVShows;

namespace MarvelAPI.Models.Characters
{
    public class CharacterDetail
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public string Age { get; set; }

        public string Location { get; set; }
        public string Origin { get; set; }
        public string Abilities { get; set; }
        public string AbilitiesOrigin { get; set; }
        public string Aliases { get; set; }
        public string Status { get; set; }

        public List<MovieListItem> Movies {get; set;}
        public List<TVShowListItem> TVShows {get; set;}
    }
}