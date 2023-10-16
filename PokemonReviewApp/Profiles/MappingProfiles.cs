using AutoMapper;
using PokemonReviewApp.DTOs;
using PokemonReviewApp.Helpers;

namespace PokemonReviewApp.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Models.Pokemon , DTOs.PokemonDto>();
              CreateMap<DTOs.PokemonDto, Models.Pokemon>();


            CreateMap<Models.Category , DTOs.CategoryDto>();
               CreateMap<DTOs.CategoryDto, Models.Category>();

            CreateMap<Models.Country , DTOs.CountryDto>(); 
               CreateMap<DTOs.CountryDto, Models.Country>();

            CreateMap<Models.Owner , DTOs.OwnerDto>();
              CreateMap<DTOs.OwnerDto, Models.Owner>(); 

            CreateMap<Models.Review , DTOs.ReviewDto>();
               CreateMap<DTOs.ReviewDto, Models.Review>();

            CreateMap<Models.Reviewer , DTOs.ReviewerDto>();
              CreateMap<DTOs.ReviewerDto, Models.Reviewer>();

            CreateMap<ApplicationUser, RegisterDto>();
               CreateMap<RegisterDto, ApplicationUser>();

            CreateMap<ApplicationUser , TokenRequestDto>();
                 CreateMap<TokenRequestDto, ApplicationUser>();


            CreateMap<ApplicationUser , RoleDto>();
                 CreateMap<RoleDto , ApplicationUser>();


        }

    }
}
