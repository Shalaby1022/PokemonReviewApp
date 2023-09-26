using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data.Interface
{
    public interface IReviewerRepsitory
    {
        ICollection<Reviewer> GetAllReviewrs();
        Reviewer GetReviewerById(int id);
        ICollection<Review> GetReviewsFromAReviewer(int Reviewrid);
        bool ReviewerExist(int id);
        bool CreateReviewer(Reviewer reviewer);
        bool UpdateReviewer(Reviewer reviewer);
        bool save();



    }
}
