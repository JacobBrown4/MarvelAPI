using System.ComponentModel.DataAnnotations;

namespace MarvelAPI.Data.Entities
{
    public class Movies
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public DateTime ReleaseYear { get; set; }
    }
}