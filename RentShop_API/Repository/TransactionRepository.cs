using Entities;
using Entities.Models;
using Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
{
    private readonly RentDbContext _context;

    public TransactionRepository(RentDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Transaction>> GetTransactions()
    {
        return await GetAll();
    }

    public async Task<Transaction> GetTransaction(Guid id)
    {
        return await GetByCondition(x => x.Id == id).FirstOrDefaultAsync();
    }


    public async Task<Order> GetOrderByTransaction(Guid transactionId)
    {
        return await _context.Transactions.Include(x => x.Order).Where(x => x.Id == transactionId).Select(x => x.Order).FirstOrDefaultAsync();
    }

    public async Task<bool> TransactionExists(Guid id)
    {
        return await Exists(id);
    }

    public async Task CreateTransaction(Transaction transaction)
    {
        await Create(transaction);
    }

    public void DeleteTransaction(Guid transactionId)
    {
        Delete(transactionId);
    }

    public void UpdateTransaction(Transaction transaction)
    {
        Update(transaction);
    }
}