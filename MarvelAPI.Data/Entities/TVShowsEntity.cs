using System.ComponentModel.DataAnnotations;

namespace MarvelAPI.Data.Entities
{
    public class TVShowsEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int? ReleaseYear { get; set; }
        public int? Seasons { get; set; }
    }
}