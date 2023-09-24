using AutoMapper;

namespace PokemonReviewApp.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Models.Pokemon , DTOs.PokemonDto>();
            CreateMap<Models.Category , DTOs.CategoryDto>();
            CreateMap<Models.Country , DTOs.CountryDto>();  
            CreateMap<Models.Owner , DTOs.OwnerDto>();


        }

    }
}
