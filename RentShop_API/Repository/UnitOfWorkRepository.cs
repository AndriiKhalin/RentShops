using AutoMapper;
using Interfaces.IRepository;
using Microsoft.Extensions.FileProviders;
using Models;

namespace Repository;

public class UnitOfWorkRepository : IUnitOfWorkRepository, IDisposable
{

    private readonly RentDbContext _context;
    private readonly IFileProvider _fileProvider;
    private readonly IMapper _mapper;
    private ICategoryRepository? _category;
    private ILogTransactionRepository _logTransaction;
    private IOrderRepository _order;
    private IRatingRepository _rating;
    private IShopRepository _shop;
    private IUserRepository? _user;
    private ITransactionRepository _transaction;
    private ITransportAvailableRepository _transportAvailable;
    private ITransportRepository _transport;
    private bool _disposedValue;

    public UnitOfWorkRepository(RentDbContext context, IFileProvider fileProvider, IMapper mapper)
    {
        _context = context;
        _fileProvider = fileProvider;
        _mapper = mapper;
    }

    public IUserRepository User
    {
        get
        {
            return _user ??= new UserRepository(_context, _fileProvider, _mapper);
        }
    }

    public ICategoryRepository Category
    {
        get
        {
            _category ??= new CategoryRepository(_context, _fileProvider, _mapper);
            return _category;
        }
    }
    public ILogTransactionRepository LogTransaction
    {
        get
        {
            if (_logTransaction == null)
            {
                _logTransaction = new LogTransactionRepository(_context, _fileProvider, _mapper);
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
                _order = new OrderRepository(_context, _fileProvider, _mapper);
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
                _rating = new RatingRepository(_context, _fileProvider, _mapper);
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
                _shop = new ShopRepository(_context, _fileProvider, _mapper);
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
                _transaction = new TransactionRepository(_context, _fileProvider, _mapper);
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
                _transportAvailable = new TransportAvailableRepository(_context, _fileProvider, _mapper);
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
                _transport = new TransportRepository(_context);
            }
            return _transport;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposedValue)
        {
            return;
        }

        if (disposing)
        {
            _context.Dispose();
        }

        _disposedValue = true;
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}