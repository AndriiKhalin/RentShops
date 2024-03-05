using AutoMapper;
using Entities;
using Entities.Models;
using Interfaces.IRepository;
using Microsoft.Extensions.FileProviders;

namespace Repository;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private RentDbContext _context;
    private readonly IFileProvider _fileProvider;
    private readonly IMapper _mapper;
    private ICategoryRepository _category;
    private ILogTransactionRepository _logTransaction;
    private IOrderRepository _order;
    private IRatingRepository _rating;
    private IShopRepository _shop;
    private IUserRepository _user;
    private ITransactionRepository _transaction;
    private ITransportAvailableRepository _transportAvailable;
    private ITransportRepository _transport;
    public UnitOfWork(RentDbContext context, IFileProvider fileProvider, IMapper mapper)
    {
        _context = context;
        _fileProvider = fileProvider;
        _mapper = mapper;
    }

    public IUserRepository User
    {
        get
        {
            if (_user == null)
            {
                _user = new UserRepository(_context);
            }
            return _user;
        }
    }

    public ICategoryRepository Category
    {
        get
        {
            if (_category == null)
            {
                _category = new CategoryRepository(_context);
            }
            return _category;
        }
    }
    public ILogTransactionRepository LogTransaction
    {
        get
        {
            if (_logTransaction == null)
            {
                _logTransaction = new LogTransactionRepository(_context);
            }
            return _logTransaction;
        }
    }
    public IOrderRepository Order
    {
        get
        {
            if (_order == null)
            {
                _order = new OrderRepository(_context);
            }
            return _order;
        }
    }
    public IRatingRepository Rating
    {
        get
        {
            if (_rating == null)
            {
                _rating = new RatingRepository(_context);
            }
            return _rating;
        }
    }
    public IShopRepository Shop
    {
        get
        {
            if (_shop == null)
            {
                _shop = new ShopRepository(_context);
            }
            return _shop;
        }
    }
    public ITransactionRepository Transaction
    {
        get
        {
            if (_transaction == null)
            {
                _transaction = new TransactionRepository(_context);
            }
            return _transaction;
        }
    }
    public ITransportAvailableRepository TransportAvailable
    {
        get
        {
            if (_transportAvailable == null)
            {
                _transportAvailable = new TransportAvailableRepository(_context);
            }
            return _transportAvailable;
        }
    }
    public ITransportRepository Transport
    {
        get
        {
            if (_transport == null)
            {
                _transport = new TransportRepository(_context, _fileProvider, _mapper);
            }
            return _transport;
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}