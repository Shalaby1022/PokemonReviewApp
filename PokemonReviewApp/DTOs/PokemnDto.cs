using System.ComponentModel.DataAnnotations;

namespace PokemonReviewApp.DTOs
{
    public class PokemonDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Birth date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }


    }
}
