using AutoMapper;
using Entities.DTO;
using Entities.Models;
using Entities;
using Interfaces.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RentShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportController : ControllerBase
    {
        private readonly IWrapperRepository _repository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;

        public TransportController(IWrapperRepository repository, RentDbContext context, IMapper mapper)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TransportDto>))]
        public async Task<IActionResult> GetTransports()
        {
            var transports = _mapper.Map<IEnumerable<TransportDto>>(await _repository.Transport.GetTransports());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(transports);
        }

        [HttpGet("{transportId}")]
        [ProducesResponseType(200, Type = typeof(TransportDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTransport(Guid transportId)
        {
            if (!await _repository.Transport.TransportExists(transportId))
            {
                return NotFound();
            }

            var transport = _mapper.Map<TransportDto>(await _repository.Transport.GetTransport(transportId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(transport);
        }

        [HttpGet("{orderId}/transport")]
        [ProducesResponseType(200, Type = typeof(Transport))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTransportByOrder(Guid orderId)
        {
            if (!await _context.Orders.AnyAsync(x => x.Id == orderId))
            {
                return NotFound();
            }

            var transportByOrder = _mapper.Map<TransportDto>(await _repository.Transport.GetTransportByOrder(orderId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(transportByOrder);
        }

        [HttpGet("{transportId}/orders")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Order>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetOrdersByTransport(Guid transportId)
        {
            if (!await _repository.Transport.TransportExists(transportId))
            {
                return NotFound();
            }

            var ordersByTransport = _mapper.Map<IEnumerable<OrderDto>>(await _repository.Transport.GetOrdersByTransport(transportId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(ordersByTransport);
        }
    }
}
