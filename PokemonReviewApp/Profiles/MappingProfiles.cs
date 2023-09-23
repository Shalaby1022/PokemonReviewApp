using AutoMapper;

namespace PokemonReviewApp.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Models.Pokemon , DTOs.PokemonDto>();

        }

    }
}
