using ASP.NET_Core_API_PokemonApp.DTO;
using ASP.NET_Core_API_PokemonApp.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_API_PokemonApp.Controllers
{
    [Route("api/countries")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;

        private readonly IMapper _mapper;

        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CountryDTO>))]
        public async Task<IActionResult> GetCountries()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countriesDTO = _mapper.Map<List<CountryDTO>>(await _countryRepository.GetCountries());

            return Ok(countriesDTO);
        }

        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(CountryDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCountryById(int countryId)
        {
            if (!await _countryRepository.CountryExists(countryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countryDTO = _mapper.Map<CountryDTO>(await _countryRepository.GetCountryById(countryId));

            return Ok(countryDTO);
        }

        [HttpGet("countryByOwner/{ownerId}")]
        [ProducesResponseType(200, Type = typeof(CountryDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCountryByOwner(int ownerId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countryDTO = _mapper.Map<CountryDTO>(await _countryRepository.GetCountryByOwner(ownerId));

            return Ok(countryDTO);
        }

        [HttpGet("ownersFromCountry/{countryId}")]
        [ProducesResponseType(200, Type = typeof(CountryDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetOwnersFromCountry(int countryId)
        {
            if (!await _countryRepository.CountryExists(countryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ownersDTO = _mapper.Map<List<OwnerDTO>>(await _countryRepository.GetOwnersFromCountry(countryId));

            return Ok(ownersDTO);
        }
    }
}
