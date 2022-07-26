using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MarvelAPI.Data.Entities
{
    public class TeamMembershipEntity
    {
        public TeamMembershipEntity() {
            Team = new TeamEntity();
            Member = new CharacterEntity();
        }

        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Team))]
        public int TeamId { get; set; }

        [Required]
        [ForeignKey(nameof(Member))]
        public int MemberId { get; set; }

        public virtual TeamEntity Team { get; set; }
        public virtual CharacterEntity Member { get; set; }
    }
}