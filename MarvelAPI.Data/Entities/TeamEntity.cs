using System.ComponentModel.DataAnnotations;

namespace MarvelAPI.Data.Entities
{
    public class TeamEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Authority { get; set; }

        public string Alignment { get; set; }

        public virtual IEnumerable<CharacterEntity> Members { get; set; }
    }
}