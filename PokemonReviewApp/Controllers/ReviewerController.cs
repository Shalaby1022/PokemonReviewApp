﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Data.Interface;
using PokemonReviewApp.DTOs;
using PokemonReviewApp.Models;
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
    }
}