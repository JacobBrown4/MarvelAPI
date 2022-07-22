namespace MarvelAPI.Models.MovieAppearance
{
    public class MovieAppearanceDetail
    {
        public int Id { get; set; }
        public int CharacterId { get; set; }
        public string Character { get; set; }
        public int MovieId { get; set; }
        public string Movie { get; set; }
    }
}