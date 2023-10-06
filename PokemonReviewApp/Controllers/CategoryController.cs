using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        //[HttpGet]
        //public IActionResult GetAllCategories()
        //{
        //    var categories = _mapper.Map<IEnumerable<CategoryDto>>(_categoryRepository.GetCategories());
        //    if(!ModelState.IsValid) { return BadRequest(ModelState); }
        //    return Ok(categories);
        //}

        [HttpGet]
        public IActionResult GetAllCategories(string? name , string? SearchQuery )
        {
            var categories = _mapper.Map<IEnumerable<CategoryDto>>(_categoryRepository.GetAllCategories(name , SearchQuery));
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(categories);
        }

        [HttpGet("{CategoryId}")]
        public IActionResult GetCategory(int CategoryId)
        {
            if(!_categoryRepository.CategoryExist(CategoryId)) { return NotFound();}
            var specificcategory = _mapper.Map<CategoryDto>(_categoryRepository.GetCategoryById(CategoryId));
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

        [HttpPost]

        public IActionResult CreateNewCategory([FromBody] CategoryDto categoryCreate)
        {
            if(categoryCreate == null) return BadRequest(ModelState);

            var category = _categoryRepository.GetCategories()
                .Where(c => c.Name.Trim().ToUpper() == categoryCreate.Name.Trim().ToUpper())
                .FirstOrDefault();

            if(category != null)
            {
                ModelState.AddModelError("", "Category with the same name already exist");
                return StatusCode(422 , ModelState);


            }

            if(!ModelState.IsValid) { return BadRequest(ModelState); }

            var categorymap = _mapper.Map<Category>(categoryCreate);

            if(!_categoryRepository.CreateCategory(categorymap))
            {
                ModelState.AddModelError("", "failed to map data");
                return StatusCode(500, ModelState);

            }

            return Ok(categorymap);
        }

        [HttpPut("{categoryId}")]

        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDto updateCategory)
        {
            if (updateCategory == null) return BadRequest(ModelState);

            if(categoryId != updateCategory.Id) return BadRequest(ModelState);

            if (!_categoryRepository.CategoryExist(categoryId)) return NotFound();

            if(!ModelState.IsValid) return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Category>(updateCategory);

            if(!_categoryRepository.UpdateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Somehting went wrong while updaing category");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully updated");

        }
        [HttpDelete("{categoryId}")]
        public IActionResult DeleteCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExist(categoryId))
            {
                return NotFound();
            }

            var categoryToDelete = _categoryRepository.GetCategoryById(categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_categoryRepository.DeleteCategory(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }

    }
}
