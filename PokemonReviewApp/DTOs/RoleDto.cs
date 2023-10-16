using System.ComponentModel.DataAnnotations;

namespace PokemonReviewApp.DTOs
{
    public class RoleDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string RoleName {  get; set; }

    }
}
