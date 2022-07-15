using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarvelAPI.Data.Entities
{
    public class MovieAppearanceEntity
    {
        [Key]
        public int Id { get; set; }
        // TODO Add code below back in, commented out for now to avoid errors
        // [Required]
        // [ForeignKey(nameof(Movie))]
        // public int MovieId { get; set; }
        // [Required]
        // [ForeignKey(nameof(Character))]
        // public int CharacterId { get; set; }
    }
}