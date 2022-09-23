using ASP.NET_Core_API_PokemonApp.Models;

namespace ASP.NET_Core_API_PokemonApp.Interfaces
{
    public interface IPokemonRepository
    {
        Task<ICollection<Pokemon>> GetPokemons();

        Task<Pokemon> GetPokemonById(int pokemonId);

        Task<Pokemon> GetPokemonByName(string name);

        Task<decimal> GetPokemonRating(int pokemonId);

        Task<bool> PokemonExists(int pokemonId);

        Task<bool> CreatePokemon(int ownerId, int categoryId, Pokemon pokemon);

        Task<bool> Save();
    }
}
