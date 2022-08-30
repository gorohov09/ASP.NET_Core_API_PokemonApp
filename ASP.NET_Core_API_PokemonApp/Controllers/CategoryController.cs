using ASP.NET_Core_API_PokemonApp.DTO;
using ASP.NET_Core_API_PokemonApp.Interfaces;
using ASP.NET_Core_API_PokemonApp.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_API_PokemonApp.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDTO>))]
        public async Task<IActionResult> GetCategories()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoriesDTO = _mapper.Map<List<CategoryDTO>>(await _categoryRepository.GetCategories());

            return Ok(categoriesDTO);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(CategoryDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            if (!await _categoryRepository.CategoryExists(categoryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryDTO = _mapper.Map<CategoryDTO>(await _categoryRepository.GetCategoryById(categoryId));

            return Ok(categoryDTO);
        }

        [HttpGet("pokemonsByCategory/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PokemonDTO>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPokemonsByCategory(int categoryId)
        {
            if (!await _categoryRepository.CategoryExists(categoryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemonsDTO = _mapper.Map<List<PokemonDTO>>(await _categoryRepository.GetPokemonByCategory(categoryId));

            return Ok(pokemonsDTO);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO categoryCreate)
        {
            if (categoryCreate == null)
                return BadRequest(ModelState);

            var category = (await _categoryRepository.GetCategories())
                .FirstOrDefault(c => c.Name.Trim().ToUpper() == categoryCreate.Name.Trim().ToUpper());

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Category>(categoryCreate);

            var result = await _categoryRepository.CreateCategory(categoryMap);

            if (!result)
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok(result);   
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            if (!await _categoryRepository.CategoryExists(categoryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _categoryRepository.DeleteCategory(categoryId);

            if (!result)
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok(result);
        }
    }
}
