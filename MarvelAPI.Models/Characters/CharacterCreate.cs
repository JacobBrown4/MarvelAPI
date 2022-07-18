using System.ComponentModel.DataAnnotations;

namespace MarvelAPI.Models.Characters
{
    public class CharacterCreate
    {
        [Required]
        [MaxLength(100, ErrorMessage = "{0} cannot be longer than {1} characters.")]
        public string FullName { get; set; }

        [Required]
        [MaxLength(40, ErrorMessage = "{0} cannot be longer than {1} characters.")]
        public string Age { get; set; }
    }
}