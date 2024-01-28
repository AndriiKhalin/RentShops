using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentShop_API.Dto;

namespace RentShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, RentDbContext context, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public async Task<IActionResult> GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(await _categoryRepository.GetCategories());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCategory(Guid categoryId)
        {
            if (!await _categoryRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }

            var category = _mapper.Map<CategoryDto>(await _categoryRepository.GetCategory(categoryId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(category);
        }

        [HttpGet("{categoryId}/transports")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Transport>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTransportsByCategory(Guid categoryId)
        {
            if (!await _categoryRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }

            var transportsByCategory = _mapper.Map<List<TransportDto>>(await _categoryRepository.GetTransportsByCategory(categoryId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(transportsByCategory);
        }

        [HttpGet("categories/{transportId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCategoryByTransport(Guid transportId)
        {
            if (!await _context.Transports.AnyAsync(x => x.Id == transportId))
            {
                return NotFound();
            }

            var categoryByTransport = _mapper.Map<CategoryDto>(await _categoryRepository.GetCategoryByTransport(transportId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(categoryByTransport);
        }
    }
}
