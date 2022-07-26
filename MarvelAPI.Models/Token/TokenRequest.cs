using System.ComponentModel.DataAnnotations;

namespace MarvelAPI.Models.Token
{
    public class TokenRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}