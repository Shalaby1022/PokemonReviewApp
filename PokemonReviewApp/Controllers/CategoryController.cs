using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Data.Interface;
using PokemonReviewApp.DTOs;

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
    }
}
