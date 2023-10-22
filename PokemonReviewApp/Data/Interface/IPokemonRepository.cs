using Microsoft.VisualStudio.Services.WebApi;
using PokemonReviewApp.Helpers;
using PokemonReviewApp.Models;
using PokemonReviewApp.ResourceParameters;

namespace PokemonReviewApp.Data.Interface
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetAllPokemons();
        //ICollection<Pokemon> GetAllPokemons(PokemonResourceParameters pokemonResourceParameters);

        Task<PageList<Pokemon>> GetAllPokemons(PokemonResourceParameters pokemonResourceParameters);

        Pokemon GetPokieById(int id);
        Pokemon GetPokieByName(string name);
        decimal GetPokemonRating(int pokieId);
        bool pokiemonExist(int pokemonId);
        bool CreatePokemon(int OwnerId , int CategoryId , Pokemon pokemon);
        bool updatePokemon(int OwnerId, int CategoryId, Pokemon pokemon);

        bool DeletePokemon(Pokemon pokemon);


        bool save();


    }
}
