using AutoMapper;
using Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Models;
using Models.DTO.TransportDTO;
using Models.Entities;


namespace Repository;

public class TransportRepository : BaseRepository<Transport>, ITransportRepository
{

    private readonly RentDbContext _context;

    public TransportRepository(RentDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<List<Transport>> GetTransports()
    {
        return GetAll().Result.OrderBy(x => x.CreatedUpdatedAt).ToListAsync();
    }

    public Task<Transport?> GetTransport(Guid id)
    {
        return GetByCondition(x => x.Id == id).FirstOrDefaultAsync();
    }


    public async Task<IEnumerable<Order>> GetOrdersByTransport(Guid transportId)
    {
        return await GetByCondition(x => x.Id == transportId).Include(x => x.Orders).SelectMany(x => x.Orders).ToListAsync();
    }

    public async Task<TransportCategory?> GetCategoryByTransport(Guid transportId)
    {
        return await GetByCondition(x => x.Id == transportId).Include(x => x.TransportCategory).Select(x => x.TransportCategory)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> TransportExists(Guid id)
    {
        return await Exists(x => x.Id == id);
    }

    public void DeleteTransport(Guid id)
    {
        Delete(id);
    }

    public async Task UpdateTransport(Transport transport)
    {
        Update(transport);
    }

    public async Task CreateTransport(Transport transport)
    {
        await Create(transport);
    }

}