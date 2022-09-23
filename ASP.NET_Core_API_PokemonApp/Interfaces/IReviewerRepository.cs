using ASP.NET_Core_API_PokemonApp.Models;

namespace ASP.NET_Core_API_PokemonApp.Interfaces
{
    public interface IReviewerRepository
    {
        Task<ICollection<Reviewer>> GetReviewers();

        Task<Reviewer> GetReviewerById(int reviewerId);

        Task<ICollection<Review>> GetReviewsByReviewer(int reviewerId);

        Task<bool> ReviewerExists(int reviewerId);

        Task<bool> CreateReviewer(Reviewer reviewer);
    }
}
