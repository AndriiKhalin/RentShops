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
    private readonly IFileProvider _fileProvider;
    private readonly IMapper _mapper;

    public TransportRepository(RentDbContext context, IFileProvider fileProvider, IMapper mapper) : base(context)
    {
        _context = context;
        _fileProvider = fileProvider;
        _mapper = mapper;
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
        return await Exists(id);
    }

    public async Task DeleteTransport(Guid id)
    {
        Delete(id);
        //fix
        var transportEntity = await GetByCondition(x => x.Id == id).FirstOrDefaultAsync();

        if (transportEntity is not null)
        {
            if (!string.IsNullOrEmpty(transportEntity.ImgUrl))
            {
                string oldsrc = transportEntity.ImgUrl;
                System.IO.File.Delete(oldsrc);
            }
        }
    }

    public async Task UpdateTransport(Guid transportId, TransportForUpdateDto transport)
    {

        //Create Service
        var transportEntity = await GetByCondition(x => x.Id == transportId).FirstOrDefaultAsync();
        var src = "";
        var root = GetPath();
        string rootImg = "/Stuff/Images/Upload/Transport/";
        var transportname = GetUniqueFileName(transport.ImgUrl.FileName);

        if (transportEntity is not null)
        {

            if (transport.ImgUrl is not null)
            {

                var directoryPath = Path.GetDirectoryName(root) + rootImg;

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                src = Path.Combine(directoryPath, transportname);

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


            var mark = transportEntity.Mark;
            var model = transportEntity.Model;
            var imageUrl = src;
            var priceMinute = transportEntity.PriceMinute;
            var maxSpeed = transportEntity.MaxSpeed;
            var maxWeight = transportEntity.MaxWeight;


            // Обновите только те свойства, которые приходят извне
            _mapper.Map(transport, transportEntity);

            //Delete

            // Восстановите неизменяемые свойства
            transportEntity.Mark = mark;
            transportEntity.Model = model;
            transportEntity.ImgUrl = imageUrl;
            transportEntity.CreatedUpdatedAt = DateTime.Now;
            transportEntity.PriceMinute = priceMinute;
            transportEntity.MaxSpeed = maxSpeed;
            transportEntity.MaxWeight = maxWeight;

            Update(transportEntity);
        }
    }

    public async Task<Transport> CreateTransport(Guid categoryId, TransportForCreateDto transport)
    {
        var categoryEntity = await _context.TransportCategories.FirstOrDefaultAsync(x => x.Id == categoryId);
        var src = "";
        string rootImg = "/Stuff/Images/Upload/Transport/";
        var transportname = GetUniqueFileName(transport.ImgUrl.FileName);

        if (transport.ImgUrl is not null)
        {
            var root = GetPath();

            var directoryPath = Path.GetDirectoryName(root) + rootImg;

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            src = Path.Combine(directoryPath, transportname);

            using (var fileStream = new FileStream(src, FileMode.Create))
            {
                await transport.ImgUrl.CopyToAsync(fileStream);
            }
        }
        var transportMap = _mapper.Map<Transport>(transport);
        transportMap.TransportCategory = categoryEntity;
        transportMap.ImgUrl = src;
        transportMap.CreatedUpdatedAt = DateTime.Now;

        await Create(transportMap);

        return transportMap;

    }

    private string GetUniqueFileName(string fileName)
    {
        var extension = Path.GetExtension(fileName);
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
        var newFileName = $"{fileNameWithoutExtension}_{Guid.NewGuid()}{extension}";
        return newFileName;
    }
    private string GetPath()
    {
        // Использовать Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)
        // для папки общего назначения

        //return @"D:\IT\My_Projects\RentShop\RentShop_UI\Stuff\Images";

        var currentDirectory = Directory.GetCurrentDirectory();
        var projectRoot = Path.GetFullPath(Path.Combine(currentDirectory, "..", ".."));
        var pathToImages = Path.Combine(projectRoot, @"RentShop_UI\src\assets\");

        return pathToImages;
    }
}