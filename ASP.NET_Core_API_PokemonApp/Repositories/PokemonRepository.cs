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

        public async Task<bool> CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity = _context.Owners.FirstOrDefault(o => o.Id == ownerId);
            var pokemonCategoryEntity = _context.Categories.FirstOrDefault(c => c.Id == categoryId);

            if (pokemonOwnerEntity == null || pokemonCategoryEntity == null)
                return false;

            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon,
            };

            _context.Add(pokemonOwner);

            var pokemonCategory = new PokemonCategory()
            {
                Category = pokemonCategoryEntity,
                Pokemon = pokemon,
            };

            _context.Add(pokemonCategory);

            _context.Add(pokemon);

            return await Save();
        }

        public async Task<Pokemon> GetPokemonById(int pokemonId) => await _context.Pokemons
                .Include(pokemon => pokemon.Reviews)
                .Include(pokemon => pokemon.PokemonOwners)
                .ThenInclude(pokemonOwner => pokemonOwner.Owner)
                .Where(p => p.Id == pokemonId)
                .AsSplitQuery()
                .FirstOrDefaultAsync();


        public async Task<Pokemon> GetPokemonByName(string name) =>
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

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
