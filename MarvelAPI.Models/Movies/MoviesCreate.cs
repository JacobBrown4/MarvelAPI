using System.ComponentModel.DataAnnotations;

namespace MarvelAPI.Models.Movies
{
    public class MoviesCreate
    {
        [Required]
        [MinLength(10, ErrorMessage = "{0} must be at least {1} characters long.")]
        [MaxLength(150, ErrorMessage = "{0} must be no more than {1} characters long.")]
        public string Title { get; set; }
    }
}