using ASP.NET_Core_API_PokemonApp.Data;
using ASP.NET_Core_API_PokemonApp.Interfaces;
using ASP.NET_Core_API_PokemonApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_API_PokemonApp.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;

        public OwnerRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Owner> GetOwnerById(int ownerId) =>
            await _context.Owners.FirstOrDefaultAsync(o => o.Id == ownerId);

        public async Task<ICollection<Owner>> GetOwnersOfAPokemon(int pokemonId) =>
            await _context.PokemonOwners
                .Where(po => po.PokemonId == pokemonId)
                .Select(po => po.Owner)
                .ToListAsync();

        public async Task<ICollection<Owner>> GetOwners() =>
            await _context.Owners.ToListAsync();

        public async Task<ICollection<Pokemon>> GetPokemonsByOwner(int ownerId) =>
            await _context.PokemonOwners
                .Where(po => po.OwnerId == ownerId)
                .Select(po => po.Pokemon)
                .ToListAsync();

        public async Task<bool> OwnerExists(int ownerId) =>
            await _context.Owners.AnyAsync(o => o.Id == ownerId);

        public async Task<bool> CreateOwner(Owner owner)
        {
            await _context.AddAsync(owner);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
