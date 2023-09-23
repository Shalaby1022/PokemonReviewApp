using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data.Interface
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetAllPokemons();

        Pokemon GetPokieById(int id);
        Pokemon GetPokieByName(string name);
        decimal GetPokemonRating(int pokieId);
        bool pokiemonExist(int pokemonId);

    }
}
