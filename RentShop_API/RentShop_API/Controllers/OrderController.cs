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
using Entities.DTO.CategoryDTO;

namespace RentShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _repository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public OrderController(IUnitOfWork repository, RentDbContext context, IMapper mapper, ILoggerManager logger)
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

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(OrderDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateOrder([FromQuery] Guid userId, [FromQuery] Guid shopId, [FromQuery] Guid transportId, [FromBody] OrderForCreateDto orderCreate)
        {
            if (orderCreate == null)
            {
                _logger.LogError("Order object is null");
                return BadRequest(ModelState);
            }

            if (!await _repository.User.UserExists(userId) || !await _repository.Shop.ShopExists(shopId) || !await _repository.Transport.TransportExists(transportId))
            {
                _logger.LogError($"Hasn't been found in db.");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }

            var orderMap = _mapper.Map<Order>(orderCreate);

            await _repository.Order.CreateOrder(userId, shopId, transportId, orderMap);
            await _repository.Save();

            _logger.LogInfo($"New Order create success");
            var createdOrder = _mapper.Map<OrderDto>(orderMap);

            return CreatedAtAction(nameof(GetOrder), new { orderId = createdOrder.Id }, createdOrder);
        }


        [HttpPut("{orderId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateOrder(Guid orderId, [FromBody] OrderForUpdateDto orderUpdate)
        {
            if (orderUpdate == null)
            {
                _logger.LogError($"Order object sent from client is null.");
                return BadRequest(ModelState);
            }

            if (!await _repository.Order.OrderExists(orderId))
            {
                _logger.LogError($"Order with id: {orderId}, hasn't been found in db.");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }

            var orderEntity = await _repository.Order.GetOrder(orderId);

            _mapper.Map(orderUpdate, orderEntity);

            _repository.Order.UpdateOrder(orderEntity);
            await _repository.Save();


            _logger.LogInfo($"Update Order with ID: {orderId}");
            return NoContent();
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            if (!await _repository.Order.OrderExists(orderId))
            {
                _logger.LogError($"Order with id: {orderId}, hasn't been found in db.");
                return NotFound();
            }


            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _repository.Order.DeleteOrder(orderId);
            await _repository.Save();

            _logger.LogInfo($"Order delete with id: {orderId} in our database");

            return NoContent();

        }
    }
}
