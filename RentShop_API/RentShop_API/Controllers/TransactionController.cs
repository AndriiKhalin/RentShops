using AutoMapper;
using Entities.Models;
using Entities;
using Interfaces.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities.DTO.OrderDTO;
using Entities.DTO.TransactionDTO;
using Interfaces.ILoggerService;

namespace RentShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IWrapperRepository _repository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public TransactionController(IWrapperRepository repository, RentDbContext context, IMapper mapper, ILoggerManager logger)
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

    }
}
