using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Data.Interface;
using PokemonReviewApp.DTOs;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [ApiController]
    [Route("api/Category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllategories()
        {
            var categories = _mapper.Map<IEnumerable<CategoryDto>>(_categoryRepository.GetCategories());
            if(!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(categories);
        }

        [HttpGet("{CategoryId}")]
        public IActionResult GetCategory(int CategoryId)
        {
            if(!_categoryRepository.CategoryExist(CategoryId)) { return NotFound();}
            var specificcategory = _mapper.Map<CategoryDto>(_categoryRepository.GetCAtegoryById(CategoryId));
            if(!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(specificcategory);
        }

        [HttpGet("pokemon/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByCategoryId(int categoryId)
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(
                _categoryRepository.GetPokemonByCategory(categoryId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(pokemons);
        }


    }
}
