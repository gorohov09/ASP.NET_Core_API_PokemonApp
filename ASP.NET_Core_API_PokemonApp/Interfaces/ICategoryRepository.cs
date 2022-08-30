using ASP.NET_Core_API_PokemonApp.Models;

namespace ASP.NET_Core_API_PokemonApp.Interfaces
{
    public interface ICategoryRepository
    {
        Task<ICollection<Category>> GetCategories();

        Task<Category> GetCategoryById(int categoryid);

        Task<ICollection<Pokemon>> GetPokemonByCategory(int categoryId);

        Task<bool> CategoryExists(int categoryId);

        Task<bool> CreateCategory(Category category);

        Task<bool> DeleteCategory(int categoryid);

        Task<bool> Save();
    }
}
