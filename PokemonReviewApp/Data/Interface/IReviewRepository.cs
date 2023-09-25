using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data.Interface
{
    public interface IReviewRepository
    {
        ICollection<Review> GetAllReviews();
        Review GetReview(int id);
        ICollection<Review> GetRviewsofAPokemon(int PokieId);
        bool ReviewExist(int reviewId);

        bool CreateReview(Review review);
        bool save();




    }
}
