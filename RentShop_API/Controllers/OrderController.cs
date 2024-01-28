using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentShop_API.Dto;

namespace RentShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, RentDbContext context, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Order>))]
        public async Task<IActionResult> GetOrders()
        {
            var orders = _mapper.Map<List<OrderDto>>(await _orderRepository.GetOrders());
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
            if (!await _orderRepository.OrderExists(orderId))
            {
                return NotFound();
            }

            var order = _mapper.Map<OrderDto>(await _orderRepository.GetOrder(orderId));
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
            if (!await _orderRepository.OrderExists(orderId))
            {
                return NotFound();
            }

            var userByOrder = _mapper.Map<UserDto>(await _orderRepository.GetUserByOrder(orderId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(userByOrder);
        }
    }
}
