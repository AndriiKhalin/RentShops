using AutoMapper;
using Entities;
using Entities.DTO.LogTransactionDTO;
using Entities.Models;
using Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Repository;

public class LogTransactionRepository : BaseRepository<LogTransaction>, ILogTransactionRepository
{
    private readonly RentDbContext _context;
    private readonly IFileProvider _fileProvider;
    private readonly IMapper _mapper;

    public LogTransactionRepository(RentDbContext context, IFileProvider fileProvider, IMapper mapper) : base(context)
    {
        _context = context;
        _fileProvider = fileProvider;
        _mapper = mapper;
    }

    public async Task<LogTransaction> CreateLogTransaction(Guid transactionId, LogTransactionForCreateDto logTransaction)
    {
        var transactionEntity = await _context.Transactions.FirstOrDefaultAsync(x => x.Id == transactionId);

        var logTransactionMap = _mapper.Map<LogTransaction>(logTransaction);

        logTransactionMap.Transaction = transactionEntity;

        await Create(logTransactionMap);
        return logTransactionMap;
    }

    public void DeleteLogTransaction(Guid logTransactionId)
    {
        Delete(logTransactionId);
    }

    public async Task<LogTransaction> GetLogTransaction(Guid id)
    {
        return await GetByCondition(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<LogTransaction>> GetLogTransactions()
    {
        return await GetAll().Result.OrderBy(x => x.CreatedUpdatedAt).ToListAsync();
    }

    public async Task<Transaction> GetTransactionByLogTransaction(Guid logTransactionId)
    {
        return await GetByCondition(x => x.Id == logTransactionId).Include(x => x.Transaction).Select(x => x.Transaction)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> LogTransactionExists(Guid id)
    {
        return await Exists(id);
    }

    public async Task UpdateLogTransaction(Guid logTransactionId, LogTransactionForUpdateDto logTransaction)
    {
        var logTransactionEntity = await GetByCondition(x => x.Id == logTransactionId).FirstOrDefaultAsync();

        _mapper.Map(logTransaction, logTransactionEntity);

        Update(logTransactionEntity);
    }
}