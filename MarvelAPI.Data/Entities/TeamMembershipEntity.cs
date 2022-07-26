using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MarvelAPI.Data.Entities
{
    public class TeamMembershipEntity
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Team))]
        public int TeamId { get; set; }

        public TeamEntity Team { get; set; }

        [Required]
        [ForeignKey(nameof(Member))]
        public int MemberId { get; set; }

        public CharacterEntity Member { get; set; }
    }
}