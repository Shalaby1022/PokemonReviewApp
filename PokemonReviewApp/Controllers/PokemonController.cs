using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Data.Interface;
using PokemonReviewApp.DTOs;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [ApiController]
    [Route("api/pokemon")]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;

        public PokemonController(IPokemonRepository pokemonRepository , IMapper mapper )
        {
            _pokemonRepository = pokemonRepository;
            _mapper = mapper;
        }

       [HttpGet]
        public ActionResult<IEnumerable<Pokemon>> GetAllPokies()
        {
            var pokies =  _mapper.Map<IEnumerable<DTOs.PokemonDto>>(_pokemonRepository.GetAllPokemons());
            if(!ModelState.IsValid) 
            {
                return BadRequest();
            }

            return Ok(pokies);
        }

        [HttpGet("{pokieId}")]
        public IActionResult GetPokieById(int pokieId)
        {
            var pokie = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokieById(pokieId));
            if(!ModelState.IsValid && pokie == null) { return BadRequest(ModelState); }
            return Ok(pokie);

        }
        [HttpGet("{pokieId}/rating")]

        public IActionResult GetRate(int pokieId)
        {
            if(!_pokemonRepository.pokiemonExist(pokieId)) return NotFound();

            var pokierate = _pokemonRepository.GetPokemonRating(pokieId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(pokierate);
            

        }

        [HttpPost]
    
        public IActionResult CreatePokemon([FromQuery] int ownerId, [FromQuery] int catId, [FromBody] PokemonDto pokemonCreate)
        {
            if (pokemonCreate == null)
                return BadRequest(ModelState);

            var pokemons = _pokemonRepository.GetAllPokemons()
                .Where(a=>a.Name.Trim().ToUpper() == pokemonCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();


            if (pokemons != null)
            {
                ModelState.AddModelError("", "Owner already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemonMap = _mapper.Map<Pokemon>(pokemonCreate);


            if (!_pokemonRepository.CreatePokemon(ownerId, catId, pokemonMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok($"Successfully created,{pokemonMap}");

        }


    }
}
