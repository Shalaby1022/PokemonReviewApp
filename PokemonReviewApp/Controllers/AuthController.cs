using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using PokemonReviewApp.Data.Interface;
using PokemonReviewApp.DTOs;
using PokemonReviewApp.Helpers;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Authorization;

namespace PokemonReviewApp.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository authRepository , IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }

       [HttpPost("register")]
        public async Task<IActionResult> RegisterNewUserAsync([FromBody] RegisterDto register)
        {
            if (register == null) return BadRequest(ModelState);


            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var result = await _authRepository.RegisterAsync(register);

            var mappedResult = _mapper.Map<AuthModel>(result);

            if (!mappedResult.IsAuthenticated)
            {
                return BadRequest(mappedResult.Message);
            }

            return Ok("Successfully Added");
        }
       [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestDto token)
        {
            if (token == null) return BadRequest(ModelState);


            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var result = await _authRepository.GetTokenAsync(token);

            var mappedResult = _mapper.Map<AuthModel>(result);

            if (!mappedResult.IsAuthenticated)
            {
                return BadRequest(mappedResult.Message);
            }

            return Ok("Successfully Loged");
        }

        [Authorize]
        [HttpPost("add-role")]
        public async Task<IActionResult> AddRoleAsync([FromBody] RoleDto roleDto)
        {
            if (roleDto == null) return BadRequest(ModelState);


            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var result = await _authRepository.AddToRolesAsync(roleDto);
            
            if(!string.IsNullOrEmpty(result)) return BadRequest(result);

            return Ok("Successfully Assigned");
        }


    }
}
