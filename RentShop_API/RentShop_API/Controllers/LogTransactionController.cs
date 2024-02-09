using AutoMapper;
using Entities.Models;
using Entities;
using Interfaces.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities.DTO.LogTransactionDTO;
using Entities.DTO.TransactionDTO;
using Interfaces.ILoggerService;

namespace RentShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogTransactionController : ControllerBase
    {
        private readonly IWrapperRepository _repository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public LogTransactionController(IWrapperRepository repository, RentDbContext context, IMapper mapper, ILoggerManager logger)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<LogTransactionDto>))]
        public async Task<IActionResult> GetLogTransactions()
        {
            var logTransactions = _mapper.Map<IEnumerable<LogTransactionDto>>(await _repository.LogTransaction.GetLogTransactions());
            _logger.LogInfo("We take all logTransactions from database");
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _logger.LogInfo("We returned all logTransactions from database");
            return Ok(logTransactions);
        }

        [HttpGet("{logTransactionId}")]
        [ProducesResponseType(200, Type = typeof(LogTransactionDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetLogTransaction(Guid logTransactionId)
        {
            if (!await _repository.LogTransaction.LogTransactionExists(logTransactionId))
            {
                _logger.LogError($"LogTransactions with id: {logTransactionId}, hasn't been found in db.");
                return NotFound();
            }

            var logTransaction = _mapper.Map<LogTransactionDto>(await _repository.LogTransaction.GetLogTransaction(logTransactionId));
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _logger.LogInfo($"Returned logTransaction with id: {logTransactionId}");
            return Ok(logTransaction);
        }

        [HttpGet("{logTransactionId}/transaction")]
        [ProducesResponseType(200, Type = typeof(TransactionDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTransactionByLogTransaction(Guid logTransactionId)
        {
            if (!await _repository.LogTransaction.LogTransactionExists(logTransactionId))
            {
                _logger.LogError($"LogTransactions with id: {logTransactionId}, hasn't been found in db.");
                return NotFound();
            }

            var transactionByLogTransaction = _mapper.Map<TransactionDto>(await _repository.LogTransaction.GetTransactionByLogTransaction(logTransactionId));
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _logger.LogInfo($"Returned Transaction by logTransaction  with id: {logTransactionId}");
            return Ok(transactionByLogTransaction);
        }
    }
}
