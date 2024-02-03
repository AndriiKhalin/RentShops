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
    public class TransactionController : ControllerBase
    {
        private readonly IWrapperRepository _repository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;

        public TransactionController(IWrapperRepository repository, RentDbContext context, IMapper mapper)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Transaction>))]
        public async Task<IActionResult> GetTransactions()
        {
            var transactions = _mapper.Map<IEnumerable<TransactionDto>>(await _repository.Transaction.GetTransactions());
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
            if (!await _repository.Transaction.TransactionExists(transactionId))
            {
                return NotFound();
            }

            var transaction = _mapper.Map<TransactionDto>(await _repository.Transaction.GetTransaction(transactionId));
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
            if (!await _repository.Transaction.TransactionExists(transactionId))
            {
                return NotFound();
            }

            var orderByTransaction = _mapper.Map<OrderDto>(await _repository.Transaction.GetOrderByTransaction(transactionId));
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

            var transactionByOrder = _mapper.Map<TransactionDto>(await _repository.Transaction.GetTransactionByOrder(orderId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(transactionByOrder);
        }
    }
}
