using ASP.NET_Core_API_PokemonApp.Models;

namespace ASP.NET_Core_API_PokemonApp.Interfaces
{
    public interface IReviewRepository
    {
        Task<ICollection<Review>> GetReviews();

        Task<Review> GetReviewById(int reviewId);

        Task<ICollection<Review>> GetReviewsOfPokemon(int pokemonId);

        Task<bool> ReviewExists(int reviewId);

        Task<bool> CreateReview(Review review);
    }
}
