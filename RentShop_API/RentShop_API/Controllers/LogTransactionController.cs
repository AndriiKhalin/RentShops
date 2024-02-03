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
    public class LogTransactionController : ControllerBase
    {
        private readonly IWrapperRepository _repository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;

        public LogTransactionController(IWrapperRepository repository, RentDbContext context, IMapper mapper)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<LogTransaction>))]
        public async Task<IActionResult> GetLogTransactions()
        {
            var logTransactions = _mapper.Map<List<LogTransactionDto>>(await _repository.LogTransaction.GetLogTransactions());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(logTransactions);
        }

        [HttpGet("{logTransactionId}")]
        [ProducesResponseType(200, Type = typeof(LogTransaction))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetLogTransaction(Guid logTransactionId)
        {
            if (!await _repository.LogTransaction.LogTransactionExists(logTransactionId))
            {
                return NotFound();
            }

            var logTransaction = _mapper.Map<LogTransactionDto>(await _repository.LogTransaction.GetLogTransaction(logTransactionId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(logTransaction);
        }

        [HttpGet("{logTransactionId}/transaction")]
        [ProducesResponseType(200, Type = typeof(Transaction))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTransactionByLogTransaction(Guid logTransactionId)
        {
            if (!await _repository.LogTransaction.LogTransactionExists(logTransactionId))
            {
                return NotFound();
            }

            var transactionByLogTransaction = _mapper.Map<TransactionDto>(await _repository.LogTransaction.GetTransactionByLogTransaction(logTransactionId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(transactionByLogTransaction);
        }
    }
}
