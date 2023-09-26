using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data.Interface
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCAtegoryById(int id);
        ICollection<Pokemon> GetPokemonByCategory(int categoryId);
        bool CategoryExist(int categoryId);
        bool CreateCategory (Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);

        bool Save();

        


    }
}
