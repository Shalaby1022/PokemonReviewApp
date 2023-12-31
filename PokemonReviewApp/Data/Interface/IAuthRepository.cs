﻿using PokemonReviewApp.DTOs;
using PokemonReviewApp.Helpers.AuthJWT;

namespace PokemonReviewApp.Data.Interface
{
    public interface IAuthRepository
    {
        Task<AuthModel> RegisterAsync(RegisterDto register);
        Task<AuthModel> GetTokenAsync(TokenRequestDto login);
        Task<string> AddToRolesAsync(RoleDto role);

    }
}
