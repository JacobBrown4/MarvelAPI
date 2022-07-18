using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarvelAPI.Data.Entities
{
    public class MovieAppearanceEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Movies Movie { get; set; }

        [Required]
        [ForeignKey(nameof(Movie))]
        public int MovieId { get; set; }

        [Required]
        public CharacterEntity Character { get; set; }

        [Required]
        [ForeignKey(nameof(Character))]
        public int CharacterId { get; set; }
    }
}