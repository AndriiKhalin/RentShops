using AutoMapper;
using Entities.DTO;
using Entities.Models;
using Entities;
using Interfaces.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RentShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportAvailableController : ControllerBase
    {
        private readonly IWrapperRepository _repository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;

        public TransportAvailableController(IWrapperRepository repository, RentDbContext context, IMapper mapper)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TransportAvailable>))]
        public async Task<IActionResult> GetTransportAvailables()
        {
            var transportAvailables = _mapper.Map<IEnumerable<TransportAvailableDto>>(await _repository.TransportAvailable.GetTransportAvailables());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(transportAvailables);
        }

        [HttpGet("{transportAvailableId}")]
        [ProducesResponseType(200, Type = typeof(TransportAvailable))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTransportAvailable(Guid transportAvailableId)
        {
            if (!await _repository.TransportAvailable.TransportAvailableExists(transportAvailableId))
            {
                return NotFound();
            }

            var transportAvailable = _mapper.Map<TransportAvailableDto>(await _repository.TransportAvailable.GetTransportAvailable(transportAvailableId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(transportAvailable);
        }

        [HttpGet("{transportAvailableId}/transport")]
        [ProducesResponseType(200, Type = typeof(Transport))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTransportByTransportAvailable(Guid transportAvailableId)
        {
            if (!await _repository.TransportAvailable.TransportAvailableExists(transportAvailableId))
            {
                return NotFound();
            }

            var transportByTransportAvailable = _mapper.Map<TransportDto>(await _repository.TransportAvailable.GetTransportByTransportAvailable(transportAvailableId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(transportByTransportAvailable);
        }

        [HttpGet("{transportAvailableId}/shop")]
        [ProducesResponseType(200, Type = typeof(Shop))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetShopByTransportAvailable(Guid transportAvailableId)
        {
            if (!await _repository.TransportAvailable.TransportAvailableExists(transportAvailableId))
            {
                return NotFound();
            }

            var shopByTransportAvailable = _mapper.Map<ShopDto>(await _repository.TransportAvailable.GetShopByTransportAvailable(transportAvailableId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(shopByTransportAvailable);
        }
    }
}
