using MarvelAPI.Data.Entities;

namespace MarvelAPI.Models.MovieAppearance
{
    public class MovieAppearanceListItem
    {
        public int Id { get; set; }
        public CharacterEntity Character { get; set; }
        public MoviesEntity Movie { get; set; }
    }
}