using ASP.NET_Core_API_PokemonApp.Data;
using ASP.NET_Core_API_PokemonApp.Interfaces;
using ASP.NET_Core_API_PokemonApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_API_PokemonApp.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CategoryExists(int categoryId) => 
            await _context.Categories.AnyAsync(c => c.Id == categoryId);

        public async Task<bool> CreateCategory(Category category)
        {
            //Change Tracker
            await _context.AddAsync(category);
            return await Save();
            
        }

        public async Task<bool> DeleteCategory(int categoryid)
        {
            var categoryEntity = await GetCategoryById(categoryid);

            if (categoryEntity == null)
                return false;

            _context.Remove(categoryEntity);

            return await Save();
        }

        public async Task<ICollection<Category>> GetCategories() =>
            await _context.Categories.ToListAsync();

        public async Task<Category> GetCategoryById(int categoryid) =>
            await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryid);

        public async Task<ICollection<Pokemon>> GetPokemonByCategory(int categoryId) =>
            await _context.PokemonCategories.Where(pc => pc.CategoryId == categoryId)
            .Select(pc => pc.Pokemon).ToListAsync();

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
