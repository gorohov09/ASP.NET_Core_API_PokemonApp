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
        [ProducesResponseType(200, Type = typeof(IEnumerable<PokemonDTO>))]
        public async Task<IActionResult> GetPokemons()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemonsDTO = _mapper.Map<List<PokemonDTO>>(await _pokemonRepository.GetPokemons());

            return Ok(pokemonsDTO);
        }

        [HttpGet("{pokemonId}")]
        [ProducesResponseType(200, Type = typeof(PokemonDetailsDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPokemonById(int pokemonId)
        {
            if (!await _pokemonRepository.PokemonExists(pokemonId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemonDTO = _mapper.Map<PokemonDetailsDTO>(await _pokemonRepository.GetPokemonById(pokemonId));

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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CreatePokemon([FromQuery]int categoryId, [FromQuery]int ownerId, [FromBody] PokemonDTO pokemonCreate)
        {
            //У нас здесь такая сложная логика в контроллере, потому что некорректно это сувать в репозиторий
            //Репозиторий нужен для взаимодействия с БД, никакой логики не должно быть
            //А можно эту логику перенести в сервис? - Открытый вопрос

            if (pokemonCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemonMap = _mapper.Map<Pokemon>(pokemonCreate);

            var result = await _pokemonRepository.CreatePokemon(ownerId, categoryId, pokemonMap);

            if (!result)
            {
                ModelState.AddModelError("", "There is no such pokemon");
                return StatusCode(500, ModelState);
            }

            return Ok(result);
        }
    }
}
