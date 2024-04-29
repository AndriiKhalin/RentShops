using AutoMapper;
using Interfaces.IRepository;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Models;
using Models.DTO.TransactionDTO;
using Models.Entities;

namespace Repository;

public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
{
    private readonly RentDbContext _context;
    private readonly IFileProvider _fileProvider;
    private readonly IMapper _mapper;

    public TransactionRepository(RentDbContext context, IFileProvider fileProvider, IMapper mapper) : base(context)
    {
        _context = context;
        _fileProvider = fileProvider;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Transaction>> GetTransactions()
    {
        return await GetAll().Result.OrderBy(x => x.Date).ToListAsync();
    }

    public async Task<Transaction> GetTransaction(Guid id)
    {
        return await GetByCondition(x => x.Id == id).FirstOrDefaultAsync();
    }


    public async Task<Order> GetOrderByTransaction(Guid transactionId)
    {
        return await GetByCondition(x => x.Id == transactionId).Include(x => x.Order).Select(x => x.Order)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> TransactionExists(Guid id)
    {
        return await Exists(x => x.Id == id);
    }

    public async Task<Transaction> CreateTransaction(Guid orderId, TransactionForCreateDto transaction)
    {
        var orderEntity = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);

        var transactionMap = _mapper.Map<Transaction>(transaction);

        transactionMap.Order = orderEntity;
        transactionMap.Date = DateTime.Now;
        transactionMap.Sum = orderEntity.Price;

        await Create(transactionMap);
        return transactionMap;
    }

    public void DeleteTransaction(Guid transactionId)
    {
        Delete(transactionId);
    }

    public async Task UpdateTransaction(Guid transactionId, TransactionForUpdateDto transaction)
    {
        var transactionEntity = await GetByCondition(x => x.Id == transactionId).FirstOrDefaultAsync();

        _mapper.Map(transaction, transactionEntity);

        transactionEntity.Date = DateTime.Now;
        transactionEntity.Sum = transactionEntity.Order.Price;

        Update(transactionEntity);
    }
}