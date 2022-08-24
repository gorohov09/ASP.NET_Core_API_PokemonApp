using ASP.NET_Core_API_PokemonApp.Models;

namespace ASP.NET_Core_API_PokemonApp.Interfaces
{
    public interface IPokemonRepository
    {
        Task<ICollection<Pokemon>> GetPokemons();
    }
}
