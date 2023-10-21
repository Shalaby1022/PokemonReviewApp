using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace PokemonReviewApp.DTOs


{
    public class OwnerDto
    {
        [OwnerFirstNameShouldBeDifferentFromLName]
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name should be between 2 and 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name should be between 2 and 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Gym is required.")]
        [StringLength(100, ErrorMessage = "Gym name cannot exceed 100 characters.")]
        public string Gym { get; set; }


    }
}
