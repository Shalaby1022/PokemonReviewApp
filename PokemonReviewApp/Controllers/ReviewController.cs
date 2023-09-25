using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Data.Interface;
using PokemonReviewApp.DTOs;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    [ApiController]
    [Route("api/Review")]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository , IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<IEnumerable<ReviewDto>>(_reviewRepository.GetAllReviews());
            if(!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(reviews);
        }

        [HttpGet("{ReviewId}")]
        public IActionResult GetOwner(int ReviewId)
        {
            if (!_reviewRepository.ReviewExist(ReviewId))
                return NotFound();

            var owner = _mapper.Map<OwnerDto>(_reviewRepository.GetReview(ReviewId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owner);
        }

        [HttpGet("pokemon/{pokieId}")]

        public IActionResult GetReviewsForaPokemon(int PokieId)
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetRviewsofAPokemon(PokieId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(reviews);

        }
    }
}
