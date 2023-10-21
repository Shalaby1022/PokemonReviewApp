using System.ComponentModel.DataAnnotations;

namespace PokemonReviewApp.DTOs
{
    public class ReviewDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Text is required.")]
        [StringLength(1000, ErrorMessage = "Text cannot exceed 1000 characters.")]
        public string Text { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

    }
}
