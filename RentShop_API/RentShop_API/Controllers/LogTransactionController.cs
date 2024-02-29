using AutoMapper;
using Entities.Models;
using Entities;
using Interfaces.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities.DTO.LogTransactionDTO;
using Entities.DTO.TransactionDTO;
using Interfaces.ILoggerService;
using Entities.DTO.CategoryDTO;

namespace RentShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogTransactionController : ControllerBase
    {
        private readonly IUnitOfWork _repository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public LogTransactionController(IUnitOfWork repository, RentDbContext context, IMapper mapper, ILoggerManager logger)
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

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(LogTransactionDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateLogTransaction([FromQuery] Guid transactionId, [FromBody] LogTransactionForCreateDto logTransactionCreate)
        {
            if (logTransactionCreate == null)
            {
                _logger.LogError("LogTransaction object is null");
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
            var logTransactionMap = _mapper.Map<LogTransaction>(logTransactionCreate);

            await _repository.LogTransaction.CreateLogTransaction(transactionId, logTransactionMap);
            await _repository.Save();

            _logger.LogInfo($"New LogTransaction create success");
            var createdLogTransaction = _mapper.Map<LogTransactionDto>(logTransactionMap);

            return CreatedAtAction(nameof(GetLogTransaction), new { logTransactionId = createdLogTransaction.Id }, createdLogTransaction);
        }


        [HttpPut("{logTransactionId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateLogTransaction(Guid logTransactionId, [FromBody] LogTransactionForUpdateDto logTransactionUpdate)
        {
            if (logTransactionUpdate == null)
            {
                _logger.LogError($"LogTransaction object sent from client is null.");
                return BadRequest(ModelState);
            }

            if (!await _repository.LogTransaction.LogTransactionExists(logTransactionId))
            {
                _logger.LogError($"LogTransaction with id: {logTransactionId}, hasn't been found in db.");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }

            var logTransactionEntity = await _repository.LogTransaction.GetLogTransaction(logTransactionId);

            _mapper.Map(logTransactionUpdate, logTransactionEntity);

            _repository.LogTransaction.UpdateLogTransaction(logTransactionEntity);
            await _repository.Save();


            _logger.LogInfo($"Update LogTransaction with ID: {logTransactionId}");
            return NoContent();
        }

        [HttpDelete("{logTransactionId}")]
        public async Task<IActionResult> DeleteLogTransaction(Guid logTransactionId)
        {
            if (!await _repository.LogTransaction.LogTransactionExists(logTransactionId))
            {
                _logger.LogError($"LogTransaction with id: {logTransactionId}, hasn't been found in db.");
                return NotFound();
            }


            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _repository.LogTransaction.DeleteLogTransaction(logTransactionId);
            await _repository.Save();

            _logger.LogInfo($"LogTransaction delete with id: {logTransactionId} in our database");

            return NoContent();

        }
    }
}
