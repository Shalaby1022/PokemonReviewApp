using PokemonReviewApp.Data;
using PokemonReviewApp.Data.Interface;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class ReviewerRepository : IReviewerRepsitory
    {
        private readonly PokemonDbContext _context;

        public ReviewerRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _context.Add(reviewer);
            return true;
        }

        public ICollection<Reviewer> GetAllReviewrs()
        {
            return _context.Reviwers.ToList();
        }

        public Reviewer GetReviewerById(int id)
        {
            var reviewer = _context.Reviwers.FirstOrDefault(p=>p.Id == id);
            if (reviewer == null) { return null; }
            return reviewer;
        }

        public ICollection<Review> GetReviewsFromAReviewer(int Reviewrid)
        {
            var reviewsOFreviewer = _context.Reviews.Where(p=>p.Id == Reviewrid).ToList();
            return reviewsOFreviewer;
        }

        public bool ReviewerExist(int id)
        {
            return _context.Reviwers.Any(p => p.Id == id);

        }

        public bool save()
        {
            var saved = _context.SaveChanges();
            return saved >0 ?true : false;
        }

        public bool UpdateReviewer(Reviewer reviewer)
        {
            _context.Update(reviewer);
            return save();
        }
    }
}
