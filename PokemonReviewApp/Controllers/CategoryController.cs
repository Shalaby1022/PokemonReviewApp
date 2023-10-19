using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PokemonReviewApp.Data.Interface;
using PokemonReviewApp.DTOs;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    /// <summary>
    /// Represents a controller for managing categories.
    /// </summary>

    [Route("api/v{version:apiVersion}/category")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiController]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryController"/> class.
        /// </summary>
        /// <param name="categoryRepository">The category repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _mapper = mapper?? throw new ArgumentNullException(nameof(mapper));
        }


        /// <summary>
        /// Gets all categories with optional filtering and Searching.
        /// </summary>
        /// <param name="name">The name of the category.</param>
        /// <param name="SearchQuery">The search query.</param>
        /// <returns>Returns a collection of categories.</returns>

        [HttpGet]
        public IActionResult GetAllCategories(string? name , string? SearchQuery )
        {
            var categories = _mapper.Map<IEnumerable<CategoryDto>>(_categoryRepository.GetAllCategories(name , SearchQuery));
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(categories);
        }

        /// <summary>
        /// Gets a specific category by ID.
        /// </summary>
        /// <param name="CategoryId">The ID of the category.</param>
        /// <returns>Returns the specified category.</returns>
        /// 
        [HttpGet("{categoryId}")]
        public IActionResult GetCategory(int categoryId)
        {
            if(!_categoryRepository.CategoryExist(categoryId)) { return NotFound();}
            var specificcategory = _mapper.Map<CategoryDto>(_categoryRepository.GetCategoryById(categoryId));
            if(!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(specificcategory);
        }


        /// <summary>
        /// Gets all Pokemon for a specific category.
        /// </summary>
        /// <param name="categoryId">The ID of the category.</param>
        /// <returns>Returns a collection of Pokemon for the specified category.</returns>

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

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="categoryCreate">The data for the new category.</param>
        /// <returns>
        /// Returns Ok with the created category if successful.
        /// Returns BadRequest if the input data is invalid or null.
        /// Returns StatusCode 422 if a category with the same name already exists.
        /// Returns StatusCode 500 if there is an issue with mapping data or creating the category.
        /// </returns>

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


        /// <summary>
        /// Updates a category by ID.
        /// </summary>
        /// <param name="categoryId">The ID of the category to update.</param>
        /// <param name="updateCategory">The updated category data.</param>
        /// <returns>Returns Ok with a success message if the update is successful.</returns>

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

        /// <summary>
        /// Deletes a category by ID.
        /// </summary>
        /// <param name="categoryId">The ID of the category to delete.</param>
        /// <returns>Returns NoContent if the deletion is successful.</returns>

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
