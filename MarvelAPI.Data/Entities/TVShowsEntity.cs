using System.ComponentModel.DataAnnotations;

namespace MarvelAPI.Data.Entities
{
    public class TVShows
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime ReleaseYear { get; set; }
        public int Seasons { get; set; }
    }
}