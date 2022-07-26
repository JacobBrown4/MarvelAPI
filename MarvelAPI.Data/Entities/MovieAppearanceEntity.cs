using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarvelAPI.Data.Entities
{
    public class MovieAppearanceEntity
    {
        public MovieAppearanceEntity() {
            Movie = new MoviesEntity();
            Character = new CharacterEntity();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Movie))]
        public int MovieId { get; set; }

        [Required]
        [ForeignKey(nameof(Character))]
        public int CharacterId { get; set; }

        public virtual MoviesEntity Movie { get; set; }
        public virtual CharacterEntity Character { get; set; }
    }
}