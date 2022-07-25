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
        [MinLength(4)]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        public string  FirstName { get; set; }
        public string LastName { get; set; }
    }
}