using ASP.NET_Core_API_PokemonApp.DTO;
using ASP.NET_Core_API_PokemonApp.Interfaces;
using ASP.NET_Core_API_PokemonApp.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_API_PokemonApp.Controllers
{
    [Route("api/pokemons")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonRepository _pokemonRepository;

        private readonly IMapper _mapper;

        public PokemonController(IPokemonRepository pokemonRepository, IMapper mapper)
        {
            _pokemonRepository = pokemonRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public async Task<IActionResult> GetPokemons()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemonsDTO = _mapper.Map<List<PokemonDTO>>(await _pokemonRepository.GetPokemons());

            return Ok(pokemonsDTO);
        }

        [HttpGet("{pokemonId}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPokemonById(int pokemonId)
        {
            if (!await _pokemonRepository.PokemonExists(pokemonId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemonDTO = _mapper.Map<PokemonDetailsDTO>(await _pokemonRepository.GetPokemon(pokemonId));

            return Ok(pokemonDTO);
        }

        [HttpGet("rating/{pokemonId}")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPokemonRatingById(int pokemonId)
        {
            if (!await _pokemonRepository.PokemonExists(pokemonId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemonRating = await _pokemonRepository.GetPokemonRating(pokemonId);

            return Ok(pokemonRating);
        }
    }
}
