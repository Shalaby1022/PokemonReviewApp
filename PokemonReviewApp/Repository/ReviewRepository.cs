using PokemonReviewApp.Data;
using PokemonReviewApp.Data.Interface;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly PokemonDbContext _context;

        public ReviewRepository(PokemonDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool CreateReview(Review review)
        {
            _context.Add(review);
            return true;

        }

        public bool DeleteReview(Review review)
        {
            _context.Remove(review);
            return save();
        }

        public ICollection<Review> GetAllReviews()
        {
            return _context.Reviews.ToList();
        }

        public Review GetReview(int id)
        {
            var reviewi = _context.Reviews.FirstOrDefault(x => x.Id == id);
            if(reviewi is null) { throw new ArgumentException(nameof(reviewi)); }
            return reviewi;
        }

        public ICollection<Review> GetRviewsofAPokemon(int PokieId)
        {
            var RofPokie = _context.Reviews.Where(p=>p.Pokemon.Id == PokieId).ToList();
            return RofPokie;
        }

        public bool ReviewExist(int reviewId)
        {
            return _context.Reviews.Any(p=>p.Id == reviewId);
        }

        public bool save()
        {
            var saved = _context.SaveChanges(); 
            return saved > 0 ? true : false;

        }

        public bool UpdateReview(Review review)
        {
          _context.Update(review);
            return save();
        }
    }
}
