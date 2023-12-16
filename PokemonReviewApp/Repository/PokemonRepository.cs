using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.WebApi;
using PokemonReviewApp.Data;
using PokemonReviewApp.Data.Interface;
using PokemonReviewApp.DTOs;
using PokemonReviewApp.Helpers;
using PokemonReviewApp.Models;
using PokemonReviewApp.ResourceParameters;
using PokemonReviewApp.Services;
using System.Net;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly PokemonDbContext _context;
        private readonly IPropertyMappingService _propertyMappingService;

        public PokemonRepository(PokemonDbContext context , IPropertyMappingService propertyMappingService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
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

        public bool DeletePokemon(Pokemon pokemon)
        {
            _context.Remove(pokemon); return save();
        }

        public ICollection<Pokemon> GetAllPokemons()
        {
            return _context.Pokemons.OrderBy(p => p.Id).ToList();
        }

        public async Task<PageList<Pokemon>> GetAllPokemons(PokemonResourceParameters pokemonResourceParameters)
        {

            if (pokemonResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(pokemonResourceParameters));
            }
                var collection = _context.Pokemons as IQueryable<Pokemon>;

                if (!string.IsNullOrEmpty(pokemonResourceParameters.name))
                {
                    pokemonResourceParameters.name = pokemonResourceParameters.name.Trim();
                    collection = collection.Where(c => c.Name == pokemonResourceParameters.name);
                }

                if (!string.IsNullOrEmpty(pokemonResourceParameters.SearchQuery))
                {
                    pokemonResourceParameters.SearchQuery = pokemonResourceParameters.SearchQuery.Trim();
                    collection = collection.Where(a => a.Name.Contains(pokemonResourceParameters.SearchQuery));

                }

            if (!string.IsNullOrEmpty(pokemonResourceParameters.OrderBy))
            {
                // Get property mapping dictionary
                var authorPrpertyMappingDictionary = _propertyMappingService.GetPropertyMapping<PokemonDto, Pokemon>();

                //














               collection = collection.ApplySort(pokemonResourceParameters.OrderBy , authorPrpertyMappingDictionary);

                collection = collection.OrderBy(a => a.Name);

            }



            //return collection
            //.Skip(pokemonResourceParameters.PageSize *(pokemonResourceParameters.PageSize - 1))
            //.Take(pokemonResourceParameters.PageSize)
            //.ToList();
            //var pagedList = await PageList<Pokemon>.CreateAsync(collection, pokemonResourceParameters.PageNumber, pokemonResourceParameters.PageSize);

            //return pagedList;

            var pagedList = PageList<Pokemon>.CreateAsync(collection, pokemonResourceParameters.PageNumber, pokemonResourceParameters.PageSize).Result;

            return pagedList;

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
            if (pokie is null)
            {
                throw new ArgumentException("Pokemon can't be Found");
            }
            return pokie;
        } 

        public Pokemon GetPokieByName(string name)
        {
            var pokiename = _context.Pokemons.FirstOrDefault(p=>p.Name == name);

            if (pokiename is null)
            {
                throw new ArgumentException("Pokemon can't be Found");
            }
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
