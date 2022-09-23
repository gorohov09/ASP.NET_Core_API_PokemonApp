using ASP.NET_Core_API_PokemonApp.DTO;
using ASP.NET_Core_API_PokemonApp.Interfaces;
using ASP.NET_Core_API_PokemonApp.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_API_PokemonApp.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;

        private readonly IPokemonRepository _pokemonRepository;

        private readonly IReviewerRepository _reviewerRepository;

        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository, IReviewerRepository reviewerRepository, IPokemonRepository pokemonRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _pokemonRepository = pokemonRepository;
            _reviewerRepository = reviewerRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDTO>))]
        public async Task<IActionResult> GetReviews()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewsDTO = _mapper.Map<List<ReviewDTO>>(await _reviewRepository.GetReviews());

            return Ok(reviewsDTO);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(ReviewDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetReviewById(int reviewId)
        {
            if (!await _reviewRepository.ReviewExists(reviewId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewDTO = _mapper.Map<ReviewDTO>(await _reviewRepository.GetReviewById(reviewId));

            return Ok(reviewDTO);
        }

        [HttpGet("reviewsOfPokemon/{pokemonId}")]
        [ProducesResponseType(200, Type = typeof(ReviewDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetReviewsOfPokemon(int pokemonId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewsDTO = _mapper.Map<List<ReviewDTO>>(await _reviewRepository.GetReviewsOfPokemon(pokemonId));

            return Ok(reviewsDTO);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CreateReview([FromQuery] int pokemonId, [FromQuery] int reviewerId, [FromBody] ReviewDTO reviewCreate)
        {
            if (reviewCreate == null)
                return BadRequest(ModelState);

            var reviewMap = _mapper.Map<Review>(reviewCreate);

            var reviewerEntity = await _reviewerRepository.GetReviewerById(reviewerId);
            var pokemonEntity = await _pokemonRepository.GetPokemonById(pokemonId);

            if (reviewerEntity == null || pokemonEntity == null)
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            reviewMap.Pokemon = pokemonEntity;
            reviewMap.Reviewer = reviewerEntity;

            var result = await _reviewRepository.CreateReview(reviewMap);

            return Ok(result);
        }
    }
}
