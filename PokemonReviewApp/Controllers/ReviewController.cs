﻿using AutoMapper;
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
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IReviewerRepsitory _reviewerRepsitory;

        public ReviewController(IReviewRepository reviewRepository , IMapper mapper , IPokemonRepository pokemonRepository , IReviewerRepsitory reviewerRepsitory )
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _pokemonRepository = pokemonRepository;
            _reviewerRepsitory = reviewerRepsitory;
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

        [HttpPost]

        public IActionResult CreateReview([FromQuery] int reviewerId, [FromQuery] int pokeId, [FromBody] ReviewDto reviewCreate)
        {
            if (reviewCreate == null)
                return BadRequest(ModelState);

            var reviews = _reviewRepository.GetAllReviews() 
                .Where(c => c.Title.Trim().ToUpper() == reviewCreate.Title.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (reviews != null)
            {
                ModelState.AddModelError("", "Review already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewMap = _mapper.Map<Review>(reviewCreate);

            reviewMap.Pokemon = _pokemonRepository.GetPokieById(pokeId);
            reviewMap.Reviewer = _reviewerRepsitory.GetReviewerById(reviewerId);


            if (!_reviewRepository.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}
