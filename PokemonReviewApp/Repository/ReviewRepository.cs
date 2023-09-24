﻿using PokemonReviewApp.Data;
using PokemonReviewApp.Data.Interface;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly PokemonDbContext _context;

        public ReviewRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public ICollection<Review> GetAllReviews()
        {
            return _context.Reviews.ToList();
        }

        public Review GetReview(int id)
        {
            var reviewi = _context.Reviews.FirstOrDefault(x => x.Id == id);
            if (reviewi == null)  {return  null; }
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
    }
}