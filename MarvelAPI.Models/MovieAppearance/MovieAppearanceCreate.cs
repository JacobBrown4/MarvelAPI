using System.ComponentModel.DataAnnotations;

namespace MarvelAPI.Models.MovieAppearance
{
    public class MovieAppearanceCreate
    {
        [Required]
        public int MovieId { get; set; }

        [Required]
        public int CharacterId { get; set; }
    }
}