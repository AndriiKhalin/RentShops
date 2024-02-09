using AutoMapper;
using Entities.Models;
using Entities;
using Interfaces.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities.DTO.ShopDTO;
using Entities.DTO.TransportAvailableDTO;
using Entities.DTO.TransportDTO;
using Interfaces.ILoggerService;

namespace RentShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportAvailableController : ControllerBase
    {
        private readonly IWrapperRepository _repository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public TransportAvailableController(IWrapperRepository repository, RentDbContext context, IMapper mapper, ILoggerManager logger)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TransportAvailableDto>))]
        public async Task<IActionResult> GetTransportAvailables()
        {
            var transportAvailables = _mapper.Map<IEnumerable<TransportAvailableDto>>(await _repository.TransportAvailable.GetTransportAvailables());
            _logger.LogInfo("We take all transportAvailables from database");
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _logger.LogInfo("We returned all transportAvailables from database");
            return Ok(transportAvailables);
        }

        [HttpGet("{transportAvailableId}")]
        [ProducesResponseType(200, Type = typeof(TransportAvailableDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTransportAvailable(Guid transportAvailableId)
        {
            if (!await _repository.TransportAvailable.TransportAvailableExists(transportAvailableId))
            {
                _logger.LogError($"TransportAvailable with id: {transportAvailableId}, hasn't been found in db.");
                return NotFound();
            }

            var transportAvailable = _mapper.Map<TransportAvailableDto>(await _repository.TransportAvailable.GetTransportAvailable(transportAvailableId));
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _logger.LogInfo($"Returned transportAvailable with id: {transportAvailable}");
            return Ok(transportAvailable);
        }

        [HttpGet("{transportAvailableId}/transport")]
        [ProducesResponseType(200, Type = typeof(TransportDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTransportByTransportAvailable(Guid transportAvailableId)
        {
            if (!await _repository.TransportAvailable.TransportAvailableExists(transportAvailableId))
            {
                _logger.LogError($"TransportAvailable with id: {transportAvailableId}, hasn't been found in db.");
                return NotFound();
            }

            var transportByTransportAvailable = _mapper.Map<TransportDto>(await _repository.TransportAvailable.GetTransportByTransportAvailable(transportAvailableId));
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _logger.LogInfo($"Returned transport by  transportAvailable with id: {transportAvailableId}");
            return Ok(transportByTransportAvailable);
        }

        [HttpGet("{transportAvailableId}/shop")]
        [ProducesResponseType(200, Type = typeof(ShopDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetShopByTransportAvailable(Guid transportAvailableId)
        {
            if (!await _repository.TransportAvailable.TransportAvailableExists(transportAvailableId))
            {
                _logger.LogError($"TransportAvailable with id: {transportAvailableId}, hasn't been found in db.");
                return NotFound();
            }

            var shopByTransportAvailable = _mapper.Map<ShopDto>(await _repository.TransportAvailable.GetShopByTransportAvailable(transportAvailableId));
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }

            _logger.LogInfo($"Returned shop by transportAvailable with id: {transportAvailableId}");
            return Ok(shopByTransportAvailable);
        }
    }
}
