using AutoMapper;
using Interfaces.IRepository;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Models;
using Models.DTO.OrderDTO;
using Models.Entities;

namespace Repository;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    private readonly RentDbContext _context;
    private readonly IFileProvider _fileProvider;
    private readonly IMapper _mapper;

    public OrderRepository(RentDbContext context, IFileProvider fileProvider, IMapper mapper) : base(context)
    {
        _context = context;
        _fileProvider = fileProvider;
        _mapper = mapper;
    }
    public async Task<Order> CreateOrder(Guid userId, Guid shopId, Guid transportId, OrderForCreateDto order)
    {
        var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        var shopEntity = await _context.Shops.FirstOrDefaultAsync(x => x.Id == shopId);
        var transportEntity = await _context.Transports.FirstOrDefaultAsync(x => x.Id == transportId);

        var orderMap = _mapper.Map<Order>(order);

        orderMap.Transport = transportEntity;
        orderMap.Shop = shopEntity;
        orderMap.User = userEntity;
        orderMap.TransportImgUrl = transportEntity.ImgUrl;
        orderMap.CreatedUpdatedAt = DateTime.Now;
        orderMap.Price = transportEntity.PriceMinute * (float)(orderMap.OrderDateTo - orderMap.OrderDateFrom).TotalMinutes;

        await Create(orderMap);
        return orderMap;
    }

    public void DeleteOrder(Guid id)
    {
        Delete(id);
    }

    public async Task<Order> GetOrder(Guid orderId)
    {
        return await GetByCondition(x => x.Id == orderId).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Order>> GetOrders()
    {
        return await GetAll().Result.OrderBy(x => x.CreatedUpdatedAt).ToListAsync();
    }

    public async Task<User> GetUserByOrder(Guid orderId)
    {
        return await GetByCondition(x => x.Id == orderId).Include(x => x.User).Select(x => x.User)
            .FirstOrDefaultAsync();
    }

    public async Task<Transport> GetTransportByOrder(Guid orderId)
    {
        return await GetByCondition(x => x.Id == orderId).Include(x => x.Transport).Select(x => x.Transport)
            .FirstOrDefaultAsync();
    }

    public async Task<Transaction> GetTransactionByOrder(Guid orderId)
    {
        return await GetByCondition(x => x.Id == orderId).Include(x => x.Transaction).Select(x => x.Transaction)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> OrderExists(Guid id)
    {
        return await Exists(id);
    }

    public async Task UpdateOrder(Guid orderId, OrderForUpdateDto order)
    {
        var orderEntity = await GetByCondition(x => x.Id == orderId).FirstOrDefaultAsync();

        var price = orderEntity.Transport.PriceMinute * (float)(orderEntity.OrderDateTo - orderEntity.OrderDateFrom).TotalMinutes;
        var img = orderEntity.TransportImgUrl;
        var orderDateFrom = orderEntity.OrderDateFrom;
        var orderDateTo = orderEntity.OrderDateTo;

        _mapper.Map(order, orderEntity);

        orderEntity.OrderDateFrom = orderDateFrom;
        orderEntity.OrderDateTo = orderDateTo;
        orderEntity.TransportImgUrl = img;
        orderEntity.CreatedUpdatedAt = DateTime.Now;
        orderEntity.Price = price;

        Update(orderEntity);
    }


}