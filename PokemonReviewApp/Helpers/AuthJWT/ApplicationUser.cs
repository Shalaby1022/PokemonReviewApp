using Microsoft.AspNetCore.Identity;

namespace PokemonReviewApp.Helpers.AuthJWT
{
    public class ApplicationUser : IdentityUser
    {
        public string FName { get; set; }
        public string LName { get; set; }
    }
}
