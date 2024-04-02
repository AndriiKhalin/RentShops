using AutoMapper;
using Entities;
using Entities.DTO.TransportDTO;
using Entities.Models;
using Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Repository;

public class TransportRepository : BaseRepository<Transport>, ITransportRepository
{
    private readonly RentDbContext _context;
    private readonly IFileProvider _fileProvider;
    private readonly IMapper _mapper;

    public TransportRepository(RentDbContext context, IFileProvider fileProvider, IMapper mapper) : base(context)
    {
        _context = context;
        _fileProvider = fileProvider;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Transport>> GetTransports()
    {
        return await GetAll().Result.OrderBy(x => x.CreatedUpdatedAt).ToListAsync();
    }

    public async Task<Transport> GetTransport(Guid id)
    {
        return await GetByCondition(x => x.Id == id).FirstOrDefaultAsync();
    }


    public async Task<IEnumerable<Order>> GetOrdersByTransport(Guid transportId)
    {
        return await GetByCondition(x => x.Id == transportId).Include(x => x.Orders).SelectMany(x => x.Orders).ToListAsync();
    }

    public async Task<TransportCategory> GetCategoryByTransport(Guid transportId)
    {
        return await GetByCondition(x => x.Id == transportId).Include(x => x.TransportCategory).Select(x => x.TransportCategory)
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

    public async Task UpdateTransport(Guid transportId, TransportForUpdateDto transport)
    {
        var transportEntity = await GetByCondition(x => x.Id == transportId).FirstOrDefaultAsync();
        var src = "";
        var root = @"D:\IT\My_Projects\RentShop\RentShop_UI\Stuff\Images\Upload\Transport\";
        string rootImg = "/Upload/Transport/";
        var transportname = $"{transport.Mark}_{transport.Model}({Guid.NewGuid()}){Path.GetExtension(transport.ImgUrl.FileName)}";

        if (transportEntity is not null)
        {

            if (transport.ImgUrl is not null)
            {


                var directoryPath = Path.GetDirectoryName(root);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                src = Path.Combine(root, transportname);

                using (var fileStream = new FileStream(src, FileMode.Create))
                {
                    await transport.ImgUrl.CopyToAsync(fileStream);
                }
            }

            if (!string.IsNullOrEmpty(transportEntity.ImgUrl))
            {
                string oldsrc = transportEntity.ImgUrl;
                System.IO.File.Delete(oldsrc);
            }

            _mapper.Map(transport, transportEntity);
            transportEntity.ImgUrl = rootImg + transportname;
            transportEntity.CreatedUpdatedAt = DateTime.Now;

            Update(transportEntity);
        }
    }

    public async Task<Transport> CreateTransport(Guid categoryId, TransportForCreateDto transport)
    {
        var categoryEntity = await _context.TransportCategories.FirstOrDefaultAsync(x => x.Id == categoryId);
        var src = "";
        string rootImg = "/Upload/Transport/";
        var transportname = $"{transport.Mark}_{transport.Model}({Guid.NewGuid()}){Path.GetExtension(transport.ImgUrl.FileName)}";

        if (transport.ImgUrl is not null)
        {
            var root = @"D:\IT\My_Projects\RentShop\RentShop_UI\Stuff\Images\Upload\Transport\";
            //var transportname = transport.ImgUrl.FileName;

            var directoryPath = Path.GetDirectoryName(root);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            src = Path.Combine(root, transportname);

            using (var fileStream = new FileStream(src, FileMode.Create))
            {
                await transport.ImgUrl.CopyToAsync(fileStream);
            }
        }
        var transportMap = _mapper.Map<Transport>(transport);
        transportMap.TransportCategory = categoryEntity;
        transportMap.ImgUrl = rootImg + transportname;
        transportMap.CreatedUpdatedAt = DateTime.Now;

        await Create(transportMap);

        return transportMap;

    }
}