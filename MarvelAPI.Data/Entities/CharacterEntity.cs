using System.ComponentModel.DataAnnotations;

namespace MarvelAPI.Data.Entities
{
    public class CharacterEntity
    {
        public CharacterEntity() {
            Movies = new List<MovieAppearanceEntity>();
            TVShows = new List<TVShowAppearanceEntity>();
            Teams = new List<TeamMembershipEntity>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "{0} cannot be longer than {1} characters.")]
        public string FullName { get; set; }

        [Required]
        [MaxLength(40, ErrorMessage = "{0} cannot be longer than {1} characters.")]
        public string Age { get; set; }

        public string Location { get; set; }
        public string Origin { get; set; }
        public string Abilities { get; set; }
        public string AbilitiesOrigin { get; set; }
        public string Aliases { get; set; }
        public string Status { get; set; }

        public virtual IEnumerable<MovieAppearanceEntity> Movies { get; set; }

        public virtual IEnumerable<TVShowAppearanceEntity> TVShows { get; set; }

        public virtual IEnumerable<TeamMembershipEntity> Teams { get; set; }
    }
}