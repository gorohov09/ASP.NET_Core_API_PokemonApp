using ASP.NET_Core_API_PokemonApp.DTO;
using ASP.NET_Core_API_PokemonApp.Interfaces;
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

        private readonly IMapper _mapper;

        public OwnerController(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
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

    }
}
