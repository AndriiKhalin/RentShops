using AutoMapper;
using Entities.Models;
using Entities;
using Interfaces.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities.DTO.OrderDTO;
using Entities.DTO.TransactionDTO;
using Entities.DTO.TransportDTO;
using Entities.DTO.UserDTO;
using Interfaces.ILoggerService;
using Microsoft.EntityFrameworkCore;

namespace RentShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IWrapperRepository _repository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public OrderController(IWrapperRepository repository, RentDbContext context, IMapper mapper, ILoggerManager logger)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OrderDto>))]
        public async Task<IActionResult> GetOrders()
        {
            var orders = _mapper.Map<IEnumerable<OrderDto>>(await _repository.Order.GetOrders());
            _logger.LogInfo("We take all orders from database");
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _logger.LogInfo("We returned all orders from database");
            return Ok(orders);
        }

        [HttpGet("{orderId}")]
        [ProducesResponseType(200, Type = typeof(OrderDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetOrder(Guid orderId)
        {
            if (!await _repository.Order.OrderExists(orderId))
            {
                _logger.LogError($"Order with id: {orderId}, hasn't been found in db.");
                return NotFound();
            }

            var order = _mapper.Map<OrderDto>(await _repository.Order.GetOrder(orderId));
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _logger.LogInfo($"Returned order with id: {orderId}");
            return Ok(order);
        }

        [HttpGet("{orderId}/user")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUserByOrder(Guid orderId)
        {
            if (!await _repository.Order.OrderExists(orderId))
            {
                _logger.LogError($"Order with id: {orderId}, hasn't been found in db.");
                return NotFound();
            }

            var userByOrder = _mapper.Map<UserDto>(await _repository.Order.GetUserByOrder(orderId));
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _logger.LogInfo($"Returned user by order with id: {orderId}");
            return Ok(userByOrder);
        }

        [HttpGet("{orderId}/transport")]
        [ProducesResponseType(200, Type = typeof(TransportDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTransportByOrder(Guid orderId)
        {
            if (!await _repository.Order.OrderExists(orderId))
            {
                _logger.LogError($"Order with id: {orderId}, hasn't been found in db.");
                return NotFound();
            }

            var transportByOrder = _mapper.Map<TransportDto>(await _repository.Order.GetTransportByOrder(orderId));
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _logger.LogInfo($"Returned transport by order with id: {orderId}");
            return Ok(transportByOrder);
        }

        [HttpGet("{orderId}/transaction")]
        [ProducesResponseType(200, Type = typeof(TransactionDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTransactionByOrder(Guid orderId)
        {
            if (!await _repository.Order.OrderExists(orderId))
            {
                _logger.LogError($"Order with id: {orderId}, hasn't been found in db.");
                return NotFound();
            }

            var transactionByOrder = _mapper.Map<TransactionDto>(await _repository.Order.GetTransactionByOrder(orderId));
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _logger.LogInfo($"Returned transaction by order with id: {orderId}");
            return Ok(transactionByOrder);
        }
    }
}
