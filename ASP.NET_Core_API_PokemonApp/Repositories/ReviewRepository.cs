using ASP.NET_Core_API_PokemonApp.Data;
using ASP.NET_Core_API_PokemonApp.Interfaces;
using ASP.NET_Core_API_PokemonApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_API_PokemonApp.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;

        public ReviewRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateReview(Review review)
        {
            await _context.AddAsync(review);
            return await Save();
        }

        public async Task<Review> GetReviewById(int reviewId) => 
            await _context.Reviews.FirstOrDefaultAsync(r => r.Id == reviewId);

        public async Task<ICollection<Review>> GetReviews() =>
            await _context.Reviews.ToListAsync();

        public async Task<ICollection<Review>> GetReviewsOfPokemon(int pokemonId) =>
            await _context.Reviews.Where(r => r.Pokemon.Id == pokemonId).ToListAsync();

        public async Task<bool> ReviewExists(int reviewId) =>
            await _context.Reviews.AnyAsync(r => r.Id == reviewId);

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
