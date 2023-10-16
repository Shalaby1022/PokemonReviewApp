using System.ComponentModel.DataAnnotations;

namespace PokemonReviewApp.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "First name is required")]
        public string FName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string PassWord { get; set; }
    }
}
