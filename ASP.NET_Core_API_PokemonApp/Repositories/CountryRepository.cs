using ASP.NET_Core_API_PokemonApp.Data;
using ASP.NET_Core_API_PokemonApp.Interfaces;
using ASP.NET_Core_API_PokemonApp.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_API_PokemonApp.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;

        public CountryRepository(DataContext context, IMapper mapper)
        {
            _context = context;
        }

        public async Task<bool> CountryExists(int countryId) =>
            await _context.Countries.AnyAsync(c => c.Id == countryId);

        public async Task<bool> CreateCountry(Country country)
        {
            await _context.AddAsync(country);
            return await Save();
        }

        public async Task<ICollection<Country>> GetCountries() =>
            await _context.Countries.ToListAsync();

        public async Task<Country> GetCountryById(int countryId) =>
            await _context.Countries.FirstOrDefaultAsync(c => c.Id == countryId);

        public async Task<Country> GetCountryByOwner(int ownerId) =>
            await _context.Owners
            .Where(o => o.Id == ownerId)
            .Select(o => o.Country)
            .FirstOrDefaultAsync();

        public async Task<ICollection<Owner>> GetOwnersFromCountry(int countryId) =>
            await _context.Owners.Where(o => o.Country.Id == countryId).ToListAsync();

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
