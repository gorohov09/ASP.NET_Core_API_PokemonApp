using ASP.NET_Core_API_PokemonApp.Models;

namespace ASP.NET_Core_API_PokemonApp.Interfaces
{
    public interface ICountryRepository
    {
        Task<ICollection<Country>> GetCountries();

        Task<Country> GetCountryById(int countryId);

        Task<Country> GetCountryByOwner(int ownerId);

        Task<ICollection<Owner>> GetOwnersFromCountry(int countryId);

        Task<bool> CountryExists(int countryId);
    }
}
