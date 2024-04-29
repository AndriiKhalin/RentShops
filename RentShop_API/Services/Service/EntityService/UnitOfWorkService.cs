//using AutoMapper;
//using Interfaces.IEntityService;
//using Interfaces.IImageService;
//using Interfaces.IRepository;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.FileProviders;
//using Models;
//using Models.Entities;
//using Repository;

//namespace Services.Service.EntityService;

//public class UnitOfWorkService : IUnitOfWorkService, IDisposable
//{
//    private readonly RentDbContext _context;
//    private IUnitOfWorkRepository _unitOfWork;
//    private IMapper _mapper;
//    private IManageImage<Transport> _manageImage;
//    private ITransportService _transport;

//    private bool _disposedValue;

//    public UnitOfWorkService(RentDbContext context, IUnitOfWorkRepository unitOfWork, IMapper mapper, IManageImage<Transport> manageImage)
//    {
//        _context = context;
//        _unitOfWork = unitOfWork;
//        _mapper = mapper;
//        _manageImage = manageImage;
//    }

//    public ITransportService Transport
//    {
//        get
//        {
//            if (_transport == null)
//            {
//                _transport = new TransportService(_unitOfWork, _mapper, _manageImage);
//            }
//            return _transport;
//        }
//    }
//    public async Task Save()
//    {
//        await _unitOfWork.Save();
//    }

//    public void Dispose()
//    {
//        Dispose(true);
//        GC.SuppressFinalize(this);
//    }

//    protected virtual void Dispose(bool disposing)
//    {
//        if (_disposedValue)
//        {
//            return;
//        }

//        if (disposing)
//        {
//            _context.Dispose();
//        }

//        _disposedValue = true;
//    }
//}