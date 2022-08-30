using ASP.NET_Core_API_PokemonApp.DTO;
using ASP.NET_Core_API_PokemonApp.Interfaces;
using ASP.NET_Core_API_PokemonApp.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_API_PokemonApp.Controllers
{
    [Route("api/owners")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerRepository _ownerRepository;

        private readonly ICountryRepository _countryRepository;

        private readonly IMapper _mapper;

        public OwnerController(IOwnerRepository ownerRepository, ICountryRepository countryRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerDTO>))]
        public async Task<IActionResult> GetOwners()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ownersDTO = _mapper.Map<List<OwnerDTO>>(await _ownerRepository.GetOwners());

            return Ok(ownersDTO);
        }

        [HttpGet("{ownerId}")]
        [ProducesResponseType(200, Type = typeof(OwnerDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetOwnerById(int ownerId)
        {
            if (!await _ownerRepository.OwnerExists(ownerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ownerDTO = _mapper.Map<OwnerDTO>(await _ownerRepository.GetOwnerById(ownerId));

            return Ok(ownerDTO);
        }

        [HttpGet("pokemonsByOwner/{ownerId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PokemonDTO>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPokemonsByOwner(int ownerId)
        {
            if (!await _ownerRepository.OwnerExists(ownerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemonsDTO = _mapper.Map<List<PokemonDTO>>(await _ownerRepository.GetPokemonsByOwner(ownerId));

            return Ok(pokemonsDTO);
        }

        [HttpPost("{countryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CreateOwner(int countryId, [FromBody] OwnerDTO ownerCreate)
        {
            //У нас здесь такая сложная логика в контроллере, потому что некорректно это сувать в репозиторий
            //Репозиторий нужен для взаимодействия с БД, никакой логики не должно быть
            //А можно эту логику перенести в сервис? - Открытый вопрос

            if (ownerCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ownerMap = _mapper.Map<Owner>(ownerCreate);

            ownerMap.Country = await _countryRepository.GetCountryById(countryId);

            if (ownerMap.Country == null)
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            var result = await _ownerRepository.CreateOwner(ownerMap);

            if (!result)
            {
                ModelState.AddModelError("", "There is no such country");
                return StatusCode(500, ModelState);
            }

            return Ok(result);
        }

    }
}
