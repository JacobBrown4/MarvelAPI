using System.ComponentModel.DataAnnotations;

namespace MarvelAPI.Models.Teams
{
    public class TeamUpdate
    {
        [MaxLength(100, ErrorMessage = "{0} cannot be longer than {1} characters.")]
        public string Name { get; set; }

        [MaxLength(100, ErrorMessage = "{0} cannot be longer than {1} characters.")]
        public string Authority { get; set; }

        [MaxLength(100, ErrorMessage = "{0} cannot be longer than {1} characters.")]
        public string Alignment { get; set; }
    }
}