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
using Entities.DTO.CategoryDTO;

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

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(TransportAvailableDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateTransportAvailable([FromQuery] Guid transportId, [FromQuery] Guid shopId, [FromBody] TransportAvailableForCreateDto transportAvailableCreate)
        {
            if (transportAvailableCreate == null)
            {
                _logger.LogError("TransportAvailable object is null");
                return BadRequest(ModelState);
            }
            if (!await _repository.Transport.TransportExists(transportId) || !await _repository.Shop.ShopExists(shopId))
            {
                _logger.LogError($"Hasn't been found in db.");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            var transportAvailableMap = _mapper.Map<TransportAvailable>(transportAvailableCreate);

            await _repository.TransportAvailable.CreateTransportAvailable(transportId, shopId, transportAvailableMap);
            await _repository.Save();

            _logger.LogInfo($"New TransportAvailable create success");
            var createdTransportAvailable = _mapper.Map<TransportAvailableDto>(transportAvailableMap);

            return CreatedAtAction(nameof(GetTransportAvailable), new { transportAvailableId = createdTransportAvailable.Id }, createdTransportAvailable);
        }


        [HttpPut("{transportAvailableId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateTransportAvailable(Guid transportAvailableId, [FromBody] TransportAvailableForUpdateDto transportAvailableUpdate)
        {
            if (transportAvailableUpdate == null)
            {
                _logger.LogError($"TransportAvailable object sent from client is null.");
                return BadRequest(ModelState);
            }

            if (!await _repository.TransportAvailable.TransportAvailableExists(transportAvailableId))
            {
                _logger.LogError($"TransportAvailable with id: {transportAvailableId}, hasn't been found in db.");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }

            var transportAvailableEntity = await _repository.TransportAvailable.GetTransportAvailable(transportAvailableId);

            _mapper.Map(transportAvailableUpdate, transportAvailableEntity);

            _repository.TransportAvailable.UpdateTransportAvailable(transportAvailableEntity);
            await _repository.Save();


            _logger.LogInfo($"Update TransportAvailable with ID: {transportAvailableId}");
            return NoContent();
        }

        [HttpDelete("{transportAvailableId}")]
        public async Task<IActionResult> DeleteTransportAvailable(Guid transportAvailableId)
        {
            if (!await _repository.TransportAvailable.TransportAvailableExists(transportAvailableId))
            {
                _logger.LogError($"TransportAvailable with id: {transportAvailableId}, hasn't been found in db.");
                return NotFound();
            }


            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _repository.TransportAvailable.DeleteTransportAvailable(transportAvailableId);
            await _repository.Save();

            _logger.LogInfo($"TransportAvailable delete with id: {transportAvailableId} in our database");

            return NoContent();

        }
    }
}
