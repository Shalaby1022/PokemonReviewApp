using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Data;
using PokemonReviewApp.Data.Interface;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly PokemonDbContext _context;

        public PokemonRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemonn)
        {
            var ownerPokemon = _context.Owners.FirstOrDefault(a => a.Id == ownerId);

            var categoryPokemon = _context.Categories.FirstOrDefault(a => a.Id == categoryId);

            var pokemonowners = new PokemonOwner()
            {
                Owner = ownerPokemon,
                Pokemon = pokemonn,

            };

            _context.Add(ownerPokemon);

            var pokemonCategory = new PokemonCategory()
            {
                Category = categoryPokemon,
                Pokemon = pokemonn,
            };
            _context.Add(categoryPokemon);
            _context.Add(pokemonn);

            return save();

        }

        public ICollection<Pokemon> GetAllPokemons()
        {
            return _context.Pokemons.OrderBy(p => p.Id).ToList();
        }

        public decimal GetPokemonRating(int pokieId)
        {
            var reviews = _context.Reviews.Where(r => r.Id == pokieId).ToList();
            if (reviews.Count <= 0) return 0;
            return (decimal)reviews.Sum(r => r.Rating) / reviews.Count;
        }

        [HttpGet]
        public Pokemon GetPokieById(int id)
        {
            var pokie = _context.Pokemons.FirstOrDefault(p => p.Id == id);
            if (pokie == null)
            {
                return null;
            }
            return pokie;
        } 

        public Pokemon GetPokieByName(string name)
        {
            var pokiename = _context.Pokemons.FirstOrDefault(p=>p.Name == name);
            if (pokiename == null) return null;
            return pokiename;
        }

        public bool pokiemonExist(int pokemonId)
        {
            var pokiefound = _context.Pokemons.Any(p=>p.Id  == pokemonId);
            return pokiefound;
        }

        public bool save()
        {
            var saved = _context.SaveChanges();
            return saved >0 ? true : false;

        }

        public bool updatePokemon(int OwnerId, int CategoryId, Pokemon pokemon)
        {
            _context.Update(pokemon);
            return save();
        }
    }
}
