using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MarvelAPI.Data.Entities;

namespace MarvelAPI.Data.Entities
{
    public class TVShowAppearanceEntity
    {
        [Key]
        public int Id { get; set; }

        public TVShowsEntity TVShow { get; set; }

        [Required]
        [ForeignKey(nameof(TVShow))]
        public int TVShowId { get; set; }

        public CharacterEntity Character { get; set; }

        [Required]
        [ForeignKey(nameof(Character))]
        public int CharacterId { get; set; }
    }
}