using AutoMapper;
using Interfaces.IEntityService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO.OrderDTO;
using Models.DTO.TransportCategoryDTO;
using Models.DTO.TransportDTO;
using Interfaces.ILoggerService;
using Interfaces.IRepository;

namespace RentShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportController : ControllerBase
    {
        private readonly ITransportService _service;
        private readonly IMapper _mapper;

        public TransportController(ITransportService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TransportDto>))]
        public async Task<IActionResult> GetTransports()
        {
            var transports = _mapper.Map<IEnumerable<TransportDto>>(await _service.GetTransports());

            return Ok(transports);
        }

        [HttpGet("{transportId}")]
        [ProducesResponseType(200, Type = typeof(TransportDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTransport(Guid transportId)
        {
            var transport = _mapper.Map<TransportDto>(await _service.GetTransport(transportId));

            return Ok(transport);
        }

        [HttpGet("{transportId}/orders")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OrderDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetOrdersByTransport(Guid transportId)
        {

            var ordersByTransport = _mapper.Map<IEnumerable<OrderDto>>(await _service.GetOrdersByTransport(transportId));

            return Ok(ordersByTransport);
        }

        [HttpGet("categories/{transportId}")]
        [ProducesResponseType(200, Type = typeof(TransportCategoryDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCategoryByTransport(Guid transportId)
        {

            var categoryByTransport = _mapper.Map<TransportCategoryDto>(await _service.GetCategoryByTransport(transportId));

            return Ok(categoryByTransport);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(TransportDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateTransport([FromQuery] Guid categoryId, [FromForm] TransportForCreateDto transportCreate)
        {

            var transportMap = await _service.CreateTransport(categoryId, transportCreate);

            var createdTransport = _mapper.Map<TransportDto>(transportMap);

            return CreatedAtAction(nameof(GetTransport), new { transportId = createdTransport.Id }, createdTransport);
        }


        [HttpPut("{transportId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateTransport(Guid transportId, [FromForm] TransportForUpdateDto transportUpdate)
        {

            await _service.UpdateTransport(transportId, transportUpdate);

            return NoContent();
        }

        [HttpDelete("{transportId}")]
        public async Task<IActionResult> DeleteTransport(Guid transportId)
        {

            await _service.DeleteTransport(transportId);

            return NoContent();

        }
    }
}
