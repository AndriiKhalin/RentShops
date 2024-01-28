using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentShop_API.Dto;
using RentShop_API.Repository;

namespace RentShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;

        public TransactionController(ITransactionRepository transactionRepository, RentDbContext context, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Transaction>))]
        public async Task<IActionResult> GetTransactions()
        {
            var transactions = _mapper.Map<List<TransactionDto>>(await _transactionRepository.GetTransactions());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(transactions);
        }

        [HttpGet("{transactionId}")]
        [ProducesResponseType(200, Type = typeof(Transaction))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTransaction(Guid transactionId)
        {
            if (!await _transactionRepository.TransactionExists(transactionId))
            {
                return NotFound();
            }

            var transaction = _mapper.Map<TransactionDto>(await _transactionRepository.GetTransaction(transactionId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(transaction);
        }

        [HttpGet("{transactionId}/order")]
        [ProducesResponseType(200, Type = typeof(Order))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetOrderByTransaction(Guid transactionId)
        {
            if (!await _transactionRepository.TransactionExists(transactionId))
            {
                return NotFound();
            }

            var orderByTransaction = _mapper.Map<OrderDto>(await _transactionRepository.GetOrderByTransaction(transactionId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(orderByTransaction);
        }

        [HttpGet("{orderId}/transaction")]
        [ProducesResponseType(200, Type = typeof(Transaction))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTransactionByOrder(Guid orderId)
        {
            if (!await _context.Orders.AnyAsync(x => x.Id == orderId))
            {
                return NotFound();
            }

            var transactionByOrder = _mapper.Map<TransactionDto>(await _transactionRepository.GetTransactionByOrder(orderId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(transactionByOrder);
        }
    }
}
