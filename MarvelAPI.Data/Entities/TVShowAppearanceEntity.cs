using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarvelAPI.Data.Entities
{
    public class TVShowAppearanceEntity
    {
        public TVShowAppearanceEntity() {
            TVShow = new TVShowsEntity();
            Character = new CharacterEntity();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(TVShow))]
        public int TVShowId { get; set; }

        [Required]
        [ForeignKey(nameof(Character))]
        public int CharacterId { get; set; }

        public virtual TVShowsEntity TVShow { get; set; }
        public virtual CharacterEntity Character { get; set; }
    }
}