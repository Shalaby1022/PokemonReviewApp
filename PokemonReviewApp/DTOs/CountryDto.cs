using System.ComponentModel.DataAnnotations;

namespace PokemonReviewApp.DTOs
{
    public class CountryDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 50 characters.")]
        public string Name { get; set; }
    }
}
