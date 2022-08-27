using ASP.NET_Core_API_PokemonApp.Data;
using ASP.NET_Core_API_PokemonApp.Interfaces;
using ASP.NET_Core_API_PokemonApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_API_PokemonApp.Repositories
{
    /// <summary>
    /// Repository - Действия с базой данных
    /// </summary>
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;

        public PokemonRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Pokemon> GetPokemon(int pokemonId) =>
            await _context.Pokemons.Where(p => p.Id == pokemonId).FirstOrDefaultAsync();

        public async Task<Pokemon> GetPokemon(string name) =>
            await _context.Pokemons.Where(p => p.Name == name).FirstOrDefaultAsync();

        public async Task<decimal> GetPokemonRating(int pokemonId)
        {
            var reviews = _context.Reviews.Where(r => r.Pokemon.Id == pokemonId);

            if (reviews.Count() <= 0)
                return 0;

            return (decimal)reviews.Sum(r => r.Rating) / reviews.Count();
        }

        public async Task<ICollection<Pokemon>> GetPokemons() =>
            await _context.Pokemons.OrderBy(p => p.Id).ToListAsync();

        public async Task<bool> PokemonExists(int pokemonId) =>
            await _context.Pokemons.AnyAsync(p => p.Id == pokemonId);
    }
}
