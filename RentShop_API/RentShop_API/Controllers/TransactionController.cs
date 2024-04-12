using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO.OrderDTO;
using Models.DTO.TransactionDTO;
using Services.Interfaces.ILoggerService;
using Services.Interfaces.IRepository;

namespace RentShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IUnitOfWork _repository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public TransactionController(IUnitOfWork repository, RentDbContext context, IMapper mapper, ILoggerManager logger)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TransactionDto>))]
        public async Task<IActionResult> GetTransactions()
        {
            var transactions = _mapper.Map<IEnumerable<TransactionDto>>(await _repository.Transaction.GetTransactions());
            _logger.LogInfo("We take all transactions from database");
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _logger.LogInfo("We returned all transaction from database");
            return Ok(transactions);
        }

        [HttpGet("{transactionId}")]
        [ProducesResponseType(200, Type = typeof(TransactionDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTransaction(Guid transactionId)
        {
            if (!await _repository.Transaction.TransactionExists(transactionId))
            {
                _logger.LogError($"Transaction with id: {transactionId}, hasn't been found in db.");
                return NotFound();
            }

            var transaction = _mapper.Map<TransactionDto>(await _repository.Transaction.GetTransaction(transactionId));
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _logger.LogInfo($"Returned transaction with id: {transactionId}");
            return Ok(transaction);
        }

        [HttpGet("{transactionId}/order")]
        [ProducesResponseType(200, Type = typeof(OrderDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetOrderByTransaction(Guid transactionId)
        {
            if (!await _repository.Transaction.TransactionExists(transactionId))
            {
                _logger.LogError($"Transaction with id: {transactionId}, hasn't been found in db.");
                return NotFound();
            }

            var orderByTransaction = _mapper.Map<OrderDto>(await _repository.Transaction.GetOrderByTransaction(transactionId));
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _logger.LogInfo($"Returned order by transaction with id: {transactionId}");
            return Ok(orderByTransaction);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(TransactionDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateTransaction([FromQuery] Guid orderId, [FromForm] TransactionForCreateDto transactionCreate)
        {
            if (transactionCreate == null)
            {
                _logger.LogError("Transaction object is null");
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

            var transactionMap = await _repository.Transaction.CreateTransaction(orderId, transactionCreate);
            await _repository.Save();

            _logger.LogInfo($"New Transaction create success");
            var createdTransaction = _mapper.Map<TransactionDto>(transactionMap);

            return CreatedAtAction(nameof(GetTransaction), new { transactionId = createdTransaction.Id }, createdTransaction);
        }


        [HttpPut("{transactionId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateTransaction(Guid transactionId, [FromForm] TransactionForUpdateDto transactionUpdate)
        {
            if (transactionUpdate == null)
            {
                _logger.LogError($"Transaction object sent from client is null.");
                return BadRequest(ModelState);
            }

            if (!await _repository.Transaction.TransactionExists(transactionId))
            {
                _logger.LogError($"Transaction with id: {transactionId}, hasn't been found in db.");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }


            await _repository.Transaction.UpdateTransaction(transactionId, transactionUpdate);
            await _repository.Save();


            _logger.LogInfo($"Update Transaction with ID: {transactionId}");
            return NoContent();
        }

        [HttpDelete("{transactionId}")]
        public async Task<IActionResult> DeleteTransaction(Guid transactionId)
        {
            if (!await _repository.Transaction.TransactionExists(transactionId))
            {
                _logger.LogError($"Transaction with id: {transactionId}, hasn't been found in db.");
                return NotFound();
            }


            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _repository.Transaction.DeleteTransaction(transactionId);
            await _repository.Save();

            _logger.LogInfo($"Transaction delete with id: {transactionId} in our database");

            return NoContent();

        }

    }
}
