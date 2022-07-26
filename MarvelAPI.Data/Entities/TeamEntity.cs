using System.ComponentModel.DataAnnotations;

namespace MarvelAPI.Data.Entities
{
    public class TeamEntity
    {
        public TeamEntity() {
            Members = new List<TeamMembershipEntity>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Authority { get; set; }

        public string Alignment { get; set; }
        
        public virtual IEnumerable<TeamMembershipEntity> Members { get; set; }
    }
}