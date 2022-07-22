using MarvelAPI.Models.Characters;

namespace MarvelAPI.Models.Movies
{
    public class MovieDetail
    {
        public int Id { get; set; }   
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public List<CharacterListItem> Characters { get; set; }
    }
}