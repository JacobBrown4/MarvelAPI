namespace MarvelAPI.Models.TeamMemberships
{
    public class TeamMembershipDetail
    {
        public int Id { get; set; }

        public int TeamId { get; set; }

        public int MemberId { get; set; }

        public string Team { get; set; }

        public string Member { get; set; }
    }
}