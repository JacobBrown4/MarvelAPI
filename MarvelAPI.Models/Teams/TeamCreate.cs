using System.ComponentModel.DataAnnotations;

namespace MarvelAPI.Models.Teams
{
    public class TeamCreate
    {
        [Required]
        [MaxLength(100, ErrorMessage = "{0} cannot be longer than {1} characters.")]
        public string Name { get; set; }
    }
}