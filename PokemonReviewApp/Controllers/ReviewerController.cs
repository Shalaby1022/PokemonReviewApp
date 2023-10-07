using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Data.Interface;
using PokemonReviewApp.DTOs;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    [Route("api/v{version:apiVersion}/Reviewer")]
    [ApiVersion("2.0")]
    [ApiController]

    public class ReviewerController : Controller
    {
        private readonly IReviewerRepsitory _reviewerRepsitory;
        private readonly IMapper _mapper;

        public ReviewerController(IReviewerRepsitory reviewerRepsitory , IMapper mapper)
        {
            _reviewerRepsitory = reviewerRepsitory ?? throw new ArgumentNullException(nameof(reviewerRepsitory));
            _mapper = mapper?? throw new ArgumentNullException(nameof(mapper));
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
        [HttpPost]
   
        public IActionResult CreateReviewer([FromBody] ReviewerDto reviewerCreate)
        {
            if (reviewerCreate == null)
                return BadRequest(ModelState);

            var country = _reviewerRepsitory.GetAllReviewrs()
                .Where(c => c.LastName.Trim().ToUpper() == reviewerCreate.LastName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (country != null)
            {
                ModelState.AddModelError("", "Reviewer already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewerMap = _mapper.Map<Reviewer>(reviewerCreate);

            if (!_reviewerRepsitory.CreateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok(reviewerMap);

        }
        [HttpPut("{reviewerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePokemon(int reviewerId, [FromBody] ReviewerDto updatedReviewer)
        {
            if (updatedReviewer == null)
                return BadRequest(ModelState);

            if (reviewerId != updatedReviewer.Id)
                return BadRequest(ModelState);

            if (!_reviewerRepsitory.ReviewerExist(reviewerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var reviewerMap = _mapper.Map<Reviewer>(updatedReviewer);

            if (!_reviewerRepsitory.UpdateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Reviewer");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("reviewRId")]

        public IActionResult DeleteReviewer(int revieweRId)
        {
            if (!_reviewerRepsitory.ReviewerExist(revieweRId)) return NotFound();

            var reviewRToDelete = _reviewerRepsitory.GetReviewerById(revieweRId);

            if(!ModelState.IsValid) return BadRequest(ModelState);

            if(!_reviewerRepsitory.DeleteReviewer(reviewRToDelete))
            {
                ModelState.AddModelError("", "Failed to Delte Reviewer");
                return StatusCode(500, ModelState);

            }
            return NoContent();

        }
    }
}
