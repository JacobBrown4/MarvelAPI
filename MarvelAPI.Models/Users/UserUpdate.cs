using System.ComponentModel.DataAnnotations;

namespace MarvelAPI.Models.Users
{
    public class UserUpdate
    {
        [Required]
        [MinLength(4)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public string  FirstName { get; set; }
        public string LastName { get; set; }
    }
}