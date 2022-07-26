using System.ComponentModel.DataAnnotations;

namespace MarvelAPI.Data.Entities
{
    public class MoviesEntity
    {
        public MoviesEntity() {
            Appearances = new List<MovieAppearanceEntity>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public int? ReleaseYear { get; set; }

        public IEnumerable<MovieAppearanceEntity> Appearances { get; set; }
    }
}