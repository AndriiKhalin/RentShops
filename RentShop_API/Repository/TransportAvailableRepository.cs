using AutoMapper;
using Entities;
using Entities.DTO.TransportAvailableDTO;
using Entities.Models;
using Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Repository;

public class TransportAvailableRepository : BaseRepository<TransportAvailable>, ITransportAvailableRepository
{
    private readonly RentDbContext _context;
    private readonly IFileProvider _fileProvider;
    private readonly IMapper _mapper;

    public TransportAvailableRepository(RentDbContext context, IFileProvider fileProvider, IMapper mapper) : base(context)
    {
        _context = context;
        _fileProvider = fileProvider;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TransportAvailable>> GetTransportAvailables()
    {
        return await GetAll().Result.OrderBy(x => x.CreatedUpdatedAt).ToListAsync();
    }

    public async Task<TransportAvailable> GetTransportAvailable(Guid id)
    {
        return await GetByCondition(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Transport> GetTransportByTransportAvailable(Guid transportAvailableId)
    {
        return await GetByCondition(x => x.Id == transportAvailableId).Include(x => x.Transport).Select(x => x.Transport)
            .FirstOrDefaultAsync();
    }

    public async Task<Shop> GetShopByTransportAvailable(Guid transportAvailableId)
    {
        return await GetByCondition(x => x.Id == transportAvailableId).Include(x => x.Shop).Select(x => x.Shop)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> TransportAvailableExists(Guid id)
    {
        return await Exists(id);
    }

    public async Task<TransportAvailable> CreateTransportAvailable(Guid transportId, Guid shopId, TransportAvailableForCreateDto transportAvailable)
    {
        var transportEntity = await _context.Transports.FirstOrDefaultAsync(x => x.Id == transportId);
        var shopEntity = await _context.Shops.FirstOrDefaultAsync(x => x.Id == shopId);

        var transportAvailableMap = _mapper.Map<TransportAvailable>(transportAvailable);

        transportAvailableMap.Transport = transportEntity;
        transportAvailableMap.Shop = shopEntity;

        await Create(transportAvailableMap);
        return transportAvailableMap;
    }

    public void DeleteTransportAvailable(Guid id)
    {
        Delete(id);
    }

    public async Task UpdateTransportAvailable(Guid transportAvailableId, TransportAvailableForUpdateDto transportAvailable)
    {
        var transportAvailableEntity = await GetByCondition(x => x.Id == transportAvailableId).FirstOrDefaultAsync();

        _mapper.Map(transportAvailable, transportAvailableEntity);

        Update(transportAvailableEntity);
    }
}