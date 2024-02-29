using Entities;
using Entities.Models;
using Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Repository;

public class TransportRepository : BaseRepository<Transport>, ITransportRepository
{
    private readonly RentDbContext _context;
    private readonly IFileProvider _fileProvider;

    public TransportRepository(RentDbContext context, IFileProvider fileProvider) : base(context)
    {
        _context = context;
        _fileProvider = fileProvider;
    }

    public async Task<IEnumerable<Transport>> GetTransports()
    {
        return await GetAll();
    }

    public async Task<Transport> GetTransport(Guid id)
    {
        return await GetByCondition(x => x.Id == id).FirstOrDefaultAsync();
    }


    public async Task<IEnumerable<Order>> GetOrdersByTransport(Guid transportId)
    {
        return await GetByCondition(x => x.Id == transportId).Include(x => x.Orders).SelectMany(x => x.Orders).ToListAsync();
    }

    public async Task<Category> GetCategoryByTransport(Guid transportId)
    {
        return await GetByCondition(x => x.Id == transportId).Include(x => x.Category).Select(x => x.Category)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> TransportExists(Guid id)
    {
        return await Exists(id);
    }

    public void DeleteTransport(Guid id)
    {
        Delete(id);
    }

    public void UpdateTransport(Transport transport)
    {
        Update(transport);
    }

    public async Task CreateTransport(Guid categoryId, Transport transport)
    {
        var categoryEntity = await _context.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);

        transport.Category = categoryEntity;

        await Create(transport);
    }
}