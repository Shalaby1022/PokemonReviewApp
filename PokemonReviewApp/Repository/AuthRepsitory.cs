using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PokemonReviewApp.Data.Interface;
using PokemonReviewApp.DTOs;
using PokemonReviewApp.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PokemonReviewApp.Repository
{
    public class AuthRepsitory : IAuthRepository
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager; 
        private readonly IMapper _mapper;
        private readonly JWT _jwt;


        public AuthRepsitory(UserManager<ApplicationUser> userManager, 
                                                         IMapper mapper, IOptions<JWT> jwt ,
                                                         RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _jwt = jwt.Value;

        }

        public async Task<AuthModel> GetTokenAsync(TokenRequestDto login)
        {
            var authMdel = new AuthModel();

            var userEmailExistence = await _userManager.FindByEmailAsync(login.Email);

            if (userEmailExistence is null || !await _userManager.CheckPasswordAsync(userEmailExistence, login.Password))
            {
                authMdel.Message = "Email or pass may be wrong BRO";
                return authMdel;
            }

            var JwtSecurityToken = await CreateJwtToken(userEmailExistence);
            var rolesList = await _userManager.GetRolesAsync(userEmailExistence);

            authMdel.Email = userEmailExistence.Email;
            authMdel.ExpireDate = JwtSecurityToken.ValidTo;
            authMdel.IsAuthenticated = true;
            authMdel.Token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken);
            authMdel.Roles = rolesList.ToList();
            authMdel.Username = userEmailExistence.UserName;

            return authMdel;

        }

        public async Task<AuthModel> RegisterAsync(RegisterDto register)
        {
            var existingUserByEmail = await _userManager.FindByEmailAsync(register.Email);
            if (existingUserByEmail is not null)
                return new AuthModel { Message = "Email is already registered" };

            var existingUserByName = await _userManager.FindByNameAsync(register.UserName);
            if (existingUserByName is not null)
                return new AuthModel { Message = $"{register.UserName} is already registered" };

            var NewUser = _mapper.Map<ApplicationUser>(register);

            var result = await _userManager.CreateAsync(NewUser, register.PassWord);
            if (!result.Succeeded)
            {
                var erros = string.Empty;

                foreach (var erro in result.Errors)
                {
                    erros += $"{erro.Description},";

                }

                return new AuthModel
                {
                    Message = "Email is already registered"
                };

            }

            await _userManager.AddToRoleAsync(NewUser, "User");

            var JwtSecurityToken = await CreateJwtToken(NewUser);
            return new AuthModel
            {
                Email = NewUser.Email,
                ExpireDate = JwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken),
                Username = NewUser.UserName
            };


        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.Durationindays),
                signingCredentials: signingCredentials);


            return jwtSecurityToken;

        }

        public async Task<string> AddToRolesAsync(RoleDto role)
        {
            var UserExistence =await _userManager.FindByIdAsync(role.UserId);

            if (UserExistence == null || !await _roleManager.RoleExistsAsync(role.RoleName))
            {
                return $"Invalid UserId or Role";
            }

            if(await _userManager.IsInRoleAsync(UserExistence , role.RoleName))
            {
                return "User already assigned to role";
            }

            var result = await _userManager.AddToRoleAsync(UserExistence, role.RoleName);
            if(!result.Succeeded)
            {
                return "SMTH went worong";
            }
            return result.ToString();
        }

    }
}
