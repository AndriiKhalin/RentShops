using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentShop_API.Dto;
using RentShop_API.Repository;


namespace RentShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogTransactionController : ControllerBase
    {
        private readonly ILogTransactionRepository _logTransactionRepository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;

        public LogTransactionController(ILogTransactionRepository logTransactionRepository, RentDbContext context, IMapper mapper)
        {
            _logTransactionRepository = logTransactionRepository;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<LogTransaction>))]
        public async Task<IActionResult> GetLogTransactions()
        {
            var logTransactions = _mapper.Map<List<LogTransactionDto>>(await _logTransactionRepository.GetLogTransactions());
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
            if (!await _logTransactionRepository.LogTransactionExists(logTransactionId))
            {
                return NotFound();
            }

            var logTransaction = _mapper.Map<LogTransactionDto>(await _logTransactionRepository.GetLogTransaction(logTransactionId));
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
            if (!await _logTransactionRepository.LogTransactionExists(logTransactionId))
            {
                return NotFound();
            }

            var transactionByLogTransaction = _mapper.Map<TransactionDto>(await _logTransactionRepository.GetTransactionByLogTransaction(logTransactionId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(transactionByLogTransaction);
        }

    }
}
