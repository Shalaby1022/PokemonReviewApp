using Microsoft.AspNetCore.Identity;

namespace PokemonReviewApp.Helpers
{
    public class ApplicationUser : IdentityUser
    {
        public string FName { get; set; }
        public string LName { get; set; }
    }
}
