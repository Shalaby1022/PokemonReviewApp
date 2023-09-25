using AutoMapper;

namespace PokemonReviewApp.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Models.Pokemon , DTOs.PokemonDto>();

            CreateMap<Models.Category , DTOs.CategoryDto>();
               CreateMap<DTOs.CategoryDto, Models.Category>();

            CreateMap<Models.Country , DTOs.CountryDto>(); 
               CreateMap<DTOs.CountryDto, Models.Country>();

            CreateMap<Models.Owner , DTOs.OwnerDto>();
              CreateMap<DTOs.OwnerDto, Models.Owner>(); 

            CreateMap<Models.Review , DTOs.ReviewDto>();
            CreateMap<Models.Reviewer , DTOs.ReviewerDto>();

        }

    }
}
