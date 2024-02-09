using AutoMapper;
using Entities;
using Entities.DTO.CategoryDTO;
using Entities.DTO.TransportDTO;
using Entities.Models;
using Interfaces.ILoggerService;
using Interfaces.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RentShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IWrapperRepository _repository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public CategoryController(IWrapperRepository repository, RentDbContext context, IMapper mapper, ILoggerManager logger)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        public async Task<IActionResult> GetCategories()
        {
            var categories = _mapper.Map<IEnumerable<CategoryDto>>(await _repository.Category.GetCategories());
            _logger.LogInfo("We take all categories from database");
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _logger.LogInfo("We returned all categories from database");
            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(CategoryDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCategory(Guid categoryId)
        {
            if (!await _repository.Category.CategoryExists(categoryId))
            {
                _logger.LogError($"Category with id: {categoryId}, hasn't been found in db.");
                return NotFound();
            }

            var category = _mapper.Map<CategoryDto>(await _repository.Category.GetCategory(categoryId));
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _logger.LogInfo($"Returned category with id: {categoryId}");
            return Ok(category);
        }

        [HttpGet("{categoryId}/transports")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TransportDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTransportsByCategory(Guid categoryId)
        {
            if (!await _repository.Category.CategoryExists(categoryId))
            {
                _logger.LogError($"Category with id: {categoryId}, hasn't been found in db.");
                return NotFound();
            }

            var transportsByCategory = _mapper.Map<IEnumerable<TransportDto>>(await _repository.Category.GetTransportsByCategory(categoryId));
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _logger.LogInfo($"Returned transports by category with id: {categoryId}");
            return Ok(transportsByCategory);
        }
    }
}
