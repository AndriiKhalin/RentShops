using AutoMapper;
using Entities.Models;
using Entities;
using Entities.DTO.CategoryDTO;
using Interfaces.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities.DTO.OrderDTO;
using Entities.DTO.TransportDTO;
using Interfaces.ILoggerService;

namespace RentShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportController : ControllerBase
    {
        private readonly IUnitOfWork _repository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public TransportController(IUnitOfWork repository, RentDbContext context, IMapper mapper, ILoggerManager logger)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TransportDto>))]
        public async Task<IActionResult> GetTransports()
        {
            var transports = _mapper.Map<IEnumerable<TransportDto>>(await _repository.Transport.GetTransports());
            _logger.LogInfo("We take transports from database");

            _logger.LogInfo("We returned all transports from database");
            return Ok(transports);
        }

        [HttpGet("{transportId}")]
        [ProducesResponseType(200, Type = typeof(TransportDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTransport(Guid transportId)
        {
            if (!await _repository.Transport.TransportExists(transportId))
            {
                _logger.LogError($"Transport with id: {transportId}, hasn't been found in db.");
                return NotFound();
            }

            var transport = _mapper.Map<TransportDto>(await _repository.Transport.GetTransport(transportId));
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _logger.LogInfo($"Returned transport with id: {transportId}");
            return Ok(transport);
        }

        [HttpGet("{transportId}/orders")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OrderDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetOrdersByTransport(Guid transportId)
        {
            if (!await _repository.Transport.TransportExists(transportId))
            {
                _logger.LogError($"Transport with id: {transportId}, hasn't been found in db.");
                return NotFound();
            }

            var ordersByTransport = _mapper.Map<IEnumerable<OrderDto>>(await _repository.Transport.GetOrdersByTransport(transportId));
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _logger.LogInfo($"Returned orders by transport with id: {transportId}");
            return Ok(ordersByTransport);
        }

        [HttpGet("categories/{transportId}")]
        [ProducesResponseType(200, Type = typeof(TransportCategoryDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCategoryByTransport(Guid transportId)
        {
            if (!await _repository.Transport.TransportExists(transportId))
            {
                _logger.LogError($"Transport with id: {transportId}, hasn't been found in db.");
                return NotFound();
            }

            var categoryByTransport = _mapper.Map<TransportCategoryDto>(await _repository.Transport.GetCategoryByTransport(transportId));
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _logger.LogInfo($"Returned category by transport with id: {transportId}");
            return Ok(categoryByTransport);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(TransportDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateTransport([FromQuery] Guid categoryId, [FromForm] TransportForCreateDto transportCreate)
        {
            if (transportCreate == null)
            {
                _logger.LogError("Transport object is null");
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

            var transportMap = await _repository.Transport.CreateTransport(categoryId, transportCreate);
            await _repository.Save();

            _logger.LogInfo($"New Transport create success");
            var createdTransport = _mapper.Map<TransportDto>(transportMap);

            return CreatedAtAction(nameof(GetTransport), new { transportId = createdTransport.Id }, createdTransport);
        }


        [HttpPut("{transportId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateTransport(Guid transportId, [FromForm] TransportForUpdateDto transportUpdate)
        {
            if (transportUpdate == null)
            {
                _logger.LogError($"Transport object sent from client is null.");
                return BadRequest(ModelState);
            }

            if (!await _repository.Transport.TransportExists(transportId))
            {
                _logger.LogError($"Transport with id: {transportId}, hasn't been found in db.");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }

            await _repository.Transport.UpdateTransport(transportId, transportUpdate);
            await _repository.Save();


            _logger.LogInfo($"Update Transport with ID: {transportId}");
            return NoContent();
        }

        [HttpDelete("{transportId}")]
        public async Task<IActionResult> DeleteTransport(Guid transportId)
        {
            if (!await _repository.Transport.TransportExists(transportId))
            {
                _logger.LogError($"Transport with id: {transportId}, hasn't been found in db.");
                return NotFound();
            }


            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            await _repository.Transport.DeleteTransport(transportId);
            await _repository.Save();

            _logger.LogInfo($"Transport delete with id: {transportId} in our database");

            return NoContent();

        }
    }
}
