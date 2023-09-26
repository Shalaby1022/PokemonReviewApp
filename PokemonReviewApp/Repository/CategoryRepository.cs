using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Data.Interface;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly PokemonDbContext _context;
        public CategoryRepository(PokemonDbContext context)
        {
            _context = context;
        }
        public bool CategoryExist(int categoryId)
        {
            var categoryfound = _context.Categories.Any(p=>p.Id == categoryId);
            return categoryfound;
        }

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return Save();


        }

        public bool DeleteCategory(Category category)
        {
            _context.Remove(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
           var categories = _context.Categories.ToList();
            return categories;
        }

        public Category GetCAtegoryById(int id)
        {
            var onecategory = _context.Categories.FirstOrDefault(p=>p.Id == id);
            if(onecategory == null) { return null; }
            return onecategory;
        }

        public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
        {
            return _context.pokemonCategories.Where(p=>p.CategoryId == categoryId).Select(c=>c.Pokemon).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;


        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }

    }
}
