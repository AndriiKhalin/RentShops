using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO.TransportCategoryDTO;
using Models.DTO.TransportDTO;
using Services.Interfaces.ILoggerService;
using Services.Interfaces.IRepository;

namespace RentShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _repository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public CategoryController(IUnitOfWork repository, RentDbContext context, IMapper mapper, ILoggerManager logger)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TransportCategoryDto>))]
        public async Task<IActionResult> GetCategories()
        {
            var categories = _mapper.Map<IEnumerable<TransportCategoryDto>>(await _repository.Category.GetCategories());
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
        [ProducesResponseType(200, Type = typeof(TransportCategoryDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCategory(Guid categoryId)
        {
            if (!await _repository.Category.CategoryExists(categoryId))
            {
                _logger.LogError($"Category with id: {categoryId}, hasn't been found in db.");
                return NotFound();
            }

            var category = _mapper.Map<TransportCategoryDto>(await _repository.Category.GetCategory(categoryId));
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


        [HttpPost]
        [ProducesResponseType(201, Type = typeof(TransportCategoryDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateCategory([FromForm] TransportCategoryForCreateDto categoryCreate)
        {
            if (categoryCreate == null)
            {
                _logger.LogError("Category object is null");
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }

            var categoryMap = await _repository.Category.CreateCategory(categoryCreate);
            await _repository.Save();

            _logger.LogInfo($"New Category create success");
            var createdCategory = _mapper.Map<TransportCategoryDto>(categoryMap);

            return CreatedAtAction(nameof(GetCategory), new { categoryId = createdCategory.Id }, createdCategory);
        }


        [HttpPut("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateCategory(Guid categoryId, [FromForm] TransportCategoryForUpdateDto categoryUpdate)
        {
            if (categoryUpdate == null)
            {
                _logger.LogError($"Category object sent from client is null.");
                return BadRequest(ModelState);
            }

            if (!await _repository.Category.CategoryExists(categoryId))
            {
                _logger.LogError($"Category with id: {categoryId}, hasn't been found in db.");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }

            await _repository.Category.UpdateCategory(categoryId, categoryUpdate);
            await _repository.Save();


            _logger.LogInfo($"Update Category with ID: {categoryId}");
            return NoContent();
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(Guid categoryId)
        {
            if (!await _repository.Category.CategoryExists(categoryId))
            {
                _logger.LogError($"Category with id: {categoryId}, hasn't been found in db.");
                return NotFound();
            }


            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _repository.Category.DeleteCategory(categoryId);
            await _repository.Save();

            _logger.LogInfo($"Category delete with id: {categoryId} in our database");

            return NoContent();

        }
    }
}
