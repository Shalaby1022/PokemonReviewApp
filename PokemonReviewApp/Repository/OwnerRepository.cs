﻿using PokemonReviewApp.Data;
using PokemonReviewApp.Data.Interface;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly PokemonDbContext _context;

        public OwnerRepository(PokemonDbContext context)
        {
            _context = context;
        }
        public ICollection<Owner> GetAllOwners()
        {
           return _context.Owners.ToList();
        }

        public Owner GetOwnerById(int ownerId)
        {
            var ownie = _context.Owners.FirstOrDefault(o => o.Id == ownerId);
            if (ownie != null) { return null; }
            return ownie;
        }

        public ICollection<Owner> GetOwnerofaPokemon(int pokieId)
        {
            var ownerofPokie = _context.PokemonOwners.Where(p=>p.PokemonId == pokieId).Select(p => p.Owner).ToList();

            return ownerofPokie;

        }

        public ICollection<Pokemon> GetPOkemonFromOwner(int ownerId)
        {
            var pokieOfOwner = _context.PokemonOwners.Where(p=>p.OwnerId == ownerId).Select(p => p.Pokemon).ToList();
            return pokieOfOwner;
        }

        public bool OwnerExist(int ownerId)
        {
            return _context.Owners.Any(p=>p.Id == ownerId);

        }
    }
}