using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Data.Interface;
using PokemonReviewApp.DTOs;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    [Route("api/Owner")]
    [ApiController]
    public class OwnerController : Controller
    {
        private readonly IOwnerRepository _ownerRepsitory;
        private readonly IMapper _mapper;
        private readonly ICountryRepository _countryRepository;

        public OwnerController(IOwnerRepository ownerRepository, IMapper mapper , ICountryRepository countryRepository)
        {
            _ownerRepsitory = ownerRepository;
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        [HttpGet]
        public IActionResult GetOwners()
        {
            var owners = _mapper.Map<List<OwnerDto>>(_ownerRepsitory.GetAllOwners());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owners);
        }

        [HttpGet("{ownerId}")]
        public IActionResult GetOwner(int ownerId)
        {
            if (!_ownerRepsitory.OwnerExist(ownerId))
                return NotFound();

            var owner = _mapper.Map<OwnerDto>(_ownerRepsitory.GetOwnerById(ownerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owner);
        }

        [HttpGet("ownerId/pokemon")]
        public IActionResult GetPokemonByOwner(int ownerId)
        {
            if (!_ownerRepsitory.OwnerExist(ownerId))
            {
                return NotFound();
            }

            var owner = _mapper.Map<List<PokemonDto>>(
                _ownerRepsitory.GetPOkemonFromOwner(ownerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owner);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOwner([FromQuery] int countryId , [FromBody] OwnerDto ownerCreate)
        {
            if (ownerCreate == null)
                return BadRequest(ModelState);

            var owners = _ownerRepsitory.GetAllOwners()
                .Where(c => c.LastName.Trim().ToUpper() == ownerCreate.LastName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (owners != null)
            {
                ModelState.AddModelError("", "Owner already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ownerMap = _mapper.Map<Owner>(ownerCreate);

            ownerMap.Country = _countryRepository.GetCountry(countryId);


            if (!_ownerRepsitory.CreateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{ownerId}")]

        public IActionResult UpdateCategory(int ownerId, [FromBody] OwnerDto updateOwner)
        {
            if (updateOwner == null) return BadRequest(ModelState);

            if (ownerId != updateOwner.Id) return BadRequest(ModelState);

            if (!_ownerRepsitory.OwnerExist(ownerId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var OwnerMap = _mapper.Map<Owner>(updateOwner);

            if (!_ownerRepsitory.UpdateOwner(OwnerMap))
            {
                ModelState.AddModelError("", "Somehting went wrong while updaing Owner");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{ownerId}")]
        public IActionResult DeleteOwner(int ownerId)
        {
            if (!_countryRepository.CountryExist(ownerId)) return NotFound();

            var countryToDelete = _countryRepository.GetCountry(ownerId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if(!_countryRepository.DeleteCountry(countryToDelete))
            {
                ModelState.AddModelError("", "Something Went wrong while Deleting Country");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
