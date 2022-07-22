namespace MarvelAPI.Models.TVShowAppearance
{
    public class TVShowAppearanceDetail
    {
        public int Id { get; set; }
        public int CharacterId { get; set; }
        public string Character {get; set;}
        public int TVShowId { get; set; }
        public string TVShow {get; set;}
    }
}