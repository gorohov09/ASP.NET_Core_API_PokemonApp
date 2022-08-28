using ASP.NET_Core_API_PokemonApp.Models;

namespace ASP.NET_Core_API_PokemonApp.Interfaces
{
    public interface IOwnerRepository
    {
        Task<ICollection<Owner>> GetOwners();

        Task<Owner> GetOwnerById(int ownerId);

        Task<ICollection<Owner>> GetOwnersOfAPokemon(int pokemonId);

        Task<ICollection<Pokemon>> GetPokemonsByOwner(int ownerId);

        Task<bool> OwnerExists(int ownerId);
    }
}
