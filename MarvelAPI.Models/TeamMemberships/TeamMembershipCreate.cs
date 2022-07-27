using System.ComponentModel.DataAnnotations;

namespace MarvelAPI.Models.TeamMemberships
{
    public class TeamMembershipCreate
    {
        [Required]
        public int TeamId { get; set; }

        [Required]
        public int MemberId { get; set; }
    }
}