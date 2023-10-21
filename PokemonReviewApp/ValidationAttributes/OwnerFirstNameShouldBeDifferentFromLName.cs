using PokemonReviewApp.DTOs;
using System.ComponentModel.DataAnnotations;

namespace PokemonReviewApp.ValidationAttributes
{
    public class OwnerFirstNameShouldBeDifferentFromLName : ValidationAttribute
    {
        public OwnerFirstNameShouldBeDifferentFromLName()
        {
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(validationContext.ObjectInstance is not OwnerDto owner)
            {
                throw new Exception($"Attribute" +
                    $"{nameof(OwnerFirstNameShouldBeDifferentFromLName)}" +
                    $"Must Be Different" +
                    $"{nameof(OwnerDto)} or derived type"
                    );
            }

            if(owner.FirstName == owner.LastName)
            {
                return new ValidationResult("First Name and Last Name Shuldn't be Equla",
                new[] { nameof(OwnerDto) });
            }
            return ValidationResult.Success;
        }
    }
}
