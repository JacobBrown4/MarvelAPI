using MarvelAPI.Models.Characters;
using MarvelAPI.Models.Teams;

namespace MarvelAPI.Models.TeamMemberships
{
    public class TeamMembershipDetail
    {
        public int Id { get; set; }

        public int TeamId { get; set; }

        public int MemberId { get; set; }

        public TeamListItem Team { get; set; }

        public CharacterListItem Member { get; set; }
    }
}