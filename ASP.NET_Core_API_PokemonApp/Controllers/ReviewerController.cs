using ASP.NET_Core_API_PokemonApp.DTO;
using ASP.NET_Core_API_PokemonApp.Interfaces;
using ASP.NET_Core_API_PokemonApp.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_API_PokemonApp.Controllers
{
    [Route("api/reviewers")]
    [ApiController]
    public class ReviewerController : ControllerBase
    {
        private readonly IReviewerRepository _reviewerRepository;

        private readonly IMapper _mapper;

        public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
        {
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewerDTO>))]
        public async Task<IActionResult> GetReviewers()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewersDTO = _mapper.Map<List<ReviewerDTO>>(await _reviewerRepository.GetReviewers());

            return Ok(reviewersDTO);
        }

        [HttpGet("{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(ReviewDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPokemonById(int reviewerId)
        {
            if (!await _reviewerRepository.ReviewerExists(reviewerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewerDTO = _mapper.Map<ReviewerDTO>(await _reviewerRepository.GetReviewerById(reviewerId));

            return Ok(reviewerDTO);
        }

        [HttpGet("reviewsByReviewer/{reviewerId}")]
        public async Task<IActionResult> GetReviewsByReviewer(int reviewerId)
        {
            if (!await _reviewerRepository.ReviewerExists(reviewerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewsDTO = _mapper.Map<List<ReviewDTO>>(await _reviewerRepository.GetReviewsByReviewer(reviewerId));

            return Ok(reviewsDTO);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CreateReviewer([FromBody]CreateReviewerDTO reviewerCreate)
        {
            if (reviewerCreate == null)
                return BadRequest(ModelState);

            var reviewerMap = _mapper.Map<Reviewer>(reviewerCreate);

            var result = await _reviewerRepository.CreateReviewer(reviewerMap);

            return Ok(result);
        }
    }
}
