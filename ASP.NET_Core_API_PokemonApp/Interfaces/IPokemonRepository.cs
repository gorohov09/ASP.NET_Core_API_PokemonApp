using ASP.NET_Core_API_PokemonApp.Models;

namespace ASP.NET_Core_API_PokemonApp.Interfaces
{
    public interface IPokemonRepository
    {
        Task<ICollection<Pokemon>> GetPokemons();

        Task<Pokemon> GetPokemon(int pokemonId);

        Task<Pokemon> GetPokemon(string name);

        Task<decimal> GetPokemonRating(int pokemonId);

        Task<bool> PokemonExists(int pokemonId);
    }
}
