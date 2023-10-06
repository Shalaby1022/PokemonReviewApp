using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Data.Interface;
using PokemonReviewApp.DTOs;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        public IActionResult GetCountries()
        {
            var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetAllCountries());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(countries);
        }
        [HttpGet("{countryId}")]
       
        public IActionResult GetCountry(int countryId)
        {
            if (!_countryRepository.CountryExist(countryId))
                return NotFound();

            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountryById(countryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(country);
        }

        [HttpGet("/owners/{ownerId}")]
     
        public IActionResult GetCountryOfAnOwner(int ownerId)
        {
            var country = _mapper.Map<CountryDto>(
                _countryRepository.GetCountryByOwner(ownerId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(country);
        }
        [HttpPost]

        public IActionResult CreateCountry( [FromBody] CountryDto country)
        {
            if (country == null) return BadRequest(ModelState);

            var country1 = _countryRepository.GetAllCountries()
                .Where(c => c.Name.Trim().ToUpper() == country.Name.Trim().ToUpper())
                .FirstOrDefault();

            if (country1 != null)
            {
                ModelState.AddModelError("", "Country with the same name already exist");
                return StatusCode(422, ModelState);


            }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var countrymap = _mapper.Map<Country>(country);

            if (!_countryRepository.CreateCountry(countrymap))
            {
                ModelState.AddModelError("", "failed to map data");
                return StatusCode(500, ModelState);

            }

            return Ok(countrymap);

        }

        [HttpPut("{contryId}")]

        public IActionResult UpdateCategory(int contryId, [FromBody] CountryDto updateCountry)
        {
            if (updateCountry == null) return BadRequest(ModelState);

            if (contryId != updateCountry.Id) return BadRequest(ModelState);

            if (!_countryRepository.CountryExist(contryId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var countrymap = _mapper.Map<Country>(updateCountry);

            if (!_countryRepository.UpdateCountry(countrymap))
            {
                ModelState.AddModelError("", "Somehting went wrong while updaing Country");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{countryId}")]
        public IActionResult DeleteCountry(int countryId)
        {
            if (!_countryRepository.CountryExist(countryId)) return NotFound();
            var countryToDelete = _countryRepository.GetCountryById(countryId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if(!_countryRepository.DeleteCountry(countryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Country");
                return StatusCode(400, ModelState);
            }

            return NoContent();


        }

    }
}
