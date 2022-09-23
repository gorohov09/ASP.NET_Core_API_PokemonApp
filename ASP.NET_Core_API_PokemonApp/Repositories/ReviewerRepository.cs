using ASP.NET_Core_API_PokemonApp.Data;
using ASP.NET_Core_API_PokemonApp.Interfaces;
using ASP.NET_Core_API_PokemonApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_API_PokemonApp.Repositories
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _context;

        public ReviewerRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateReviewer(Reviewer reviewer)
        {
            await _context.AddAsync(reviewer);
            return await Save();
        }

        public async Task<Reviewer> GetReviewerById(int reviewerId) =>
            await _context.Reviewers
                .Include(r => r.Reviews)
                .FirstOrDefaultAsync(r => r.Id == reviewerId);

        public async Task<ICollection<Reviewer>> GetReviewers() =>
            await _context.Reviewers.ToListAsync();

        public async Task<ICollection<Review>> GetReviewsByReviewer(int reviewerId) =>
            await _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToListAsync();

        public async Task<bool> ReviewerExists(int reviewerId) =>
            await _context.Reviewers.AnyAsync(r => r.Id == reviewerId);

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
