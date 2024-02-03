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
    public class OrderController : ControllerBase
    {
        private readonly IWrapperRepository _repository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;

        public OrderController(IWrapperRepository repository, RentDbContext context, IMapper mapper)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Order>))]
        public async Task<IActionResult> GetOrders()
        {
            var orders = _mapper.Map<IEnumerable<OrderDto>>(await _repository.Order.GetOrders());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(orders);
        }

        [HttpGet("{orderId}")]
        [ProducesResponseType(200, Type = typeof(Order))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetOrder(Guid orderId)
        {
            if (!await _repository.Order.OrderExists(orderId))
            {
                return NotFound();
            }

            var order = _mapper.Map<OrderDto>(await _repository.Order.GetOrder(orderId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(order);
        }

        [HttpGet("{orderId}/user")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUserByOrder(Guid orderId)
        {
            if (!await _repository.Order.OrderExists(orderId))
            {
                return NotFound();
            }

            var userByOrder = _mapper.Map<UserDto>(await _repository.Order.GetUserByOrder(orderId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(userByOrder);
        }
    }
}
