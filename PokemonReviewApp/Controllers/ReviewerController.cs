using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Data.Interface;
using PokemonReviewApp.DTOs;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    [ApiController]
    [Route("api/Reviewer")]
    public class ReviewerController : Controller
    {
        private readonly IReviewerRepsitory _reviewerRepsitory;
        private readonly IMapper _mapper;

        public ReviewerController(IReviewerRepsitory reviewerRepsitory , IMapper mapper)
        {
            _reviewerRepsitory = reviewerRepsitory;
            _mapper = mapper;
        }
       [HttpGet]
       public IActionResult GetReviewers()
        {
            var reviewers = _mapper.Map<List<ReviewerDto>>(_reviewerRepsitory.GetAllReviewrs());
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

                return Ok(reviewers);
        }
        [HttpGet("{reviewerId}")]
        public IActionResult GetReviewer(int reviewerId)
        {
            if(!_reviewerRepsitory.ReviewerExist(reviewerId)) { return NotFound();  }
            var reviewer = _mapper.Map<ReviewerDto>(_reviewerRepsitory.GetReviewerById(reviewerId));
            if(!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(reviewer);
        }

        [HttpGet("{reviewerId}/reviews")]
        public IActionResult GetReviewsByAReviewer(int reviewerId)
        {
            if (!_reviewerRepsitory.ReviewerExist(reviewerId))
                return NotFound();

            var reviews = _mapper.Map<List<ReviewDto>>(
                _reviewerRepsitory.GetReviewsFromAReviewer(reviewerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }
    }
}
