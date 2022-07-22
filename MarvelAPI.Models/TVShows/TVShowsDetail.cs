using MarvelAPI.Models.Characters;
namespace MarvelAPI.Models.TVShows
{
    public class TVShowDetail
    {
        public int Id { get; set; }   
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public int Seasons { get; set; }
        public List<CharacterListItem> Characters { get; set; }
    }
}