using AutoMapper;
using Interfaces.IEntityService;
using Interfaces.IImageService;
using Interfaces.ILoggerService;
using Interfaces.IRepository;
using Models.DTO.TransportDTO;
using Models.Entities;
using Services.Service.ImageService;

namespace Services.Service.EntityService;

public class TransportService : ITransportService
{
    private readonly IUnitOfWorkRepository _unitOfWorkRep;
    private readonly IMapper _mapper;
    private readonly IManageImage<Transport> _manageImage;
    private readonly ILoggerManager _logger;

    public TransportService(IUnitOfWorkRepository unitOfWorkRep, IMapper mapper, IManageImage<Transport> manageImage, ILoggerManager logger)
    {
        _unitOfWorkRep = unitOfWorkRep;
        _mapper = mapper;
        _manageImage = manageImage;
        _logger = logger;
    }

    public async Task<List<Transport>> GetTransports()
    {
        return await _unitOfWorkRep.Transport.GetTransports();
    }

    public async Task<Transport?> GetTransport(Guid id)
    {
        if (!await TransportExists(id))
        {
            _logger.LogError($"Transport with id: {id}, hasn't been found in db.");
            throw new ArgumentNullException("Invalid transport Id");
        }

        return await _unitOfWorkRep.Transport.GetTransport(id);
    }

    public async Task<TransportCategory?> GetCategoryByTransport(Guid transportId)
    {
        if (!await TransportExists(transportId))
        {
            _logger.LogError($"Transport with id: {transportId}, hasn't been found in db.");
            throw new ArgumentNullException("Invalid transport Id");
        }


        return await _unitOfWorkRep.Transport.GetCategoryByTransport(transportId);
    }

    public async Task<IEnumerable<Order>> GetOrdersByTransport(Guid transportId)
    {
        if (!await TransportExists(transportId))
        {
            _logger.LogError($"Transport with id: {transportId}, hasn't been found in db.");
            throw new ArgumentNullException("Invalid transport Id");
        }


        return await _unitOfWorkRep.Transport.GetOrdersByTransport(transportId);
    }

    public async Task<bool> TransportExists(Guid id)
    {
        return await _unitOfWorkRep.Transport.TransportExists(id);
    }

    public async Task DeleteTransport(Guid id)
    {
        if (!await TransportExists(id))
        {
            _logger.LogError($"Transport with id: {id}, hasn't been found in db.");
            throw new ArgumentNullException("Invalid transport Id");
        }

        var transportEntityForDelete = await GetTransport(id);

        _manageImage.DeleteFile(transportEntityForDelete.ImgUrl);

        _unitOfWorkRep.Transport.DeleteTransport(id);

        await _unitOfWorkRep.Save();
    }

    public async Task UpdateTransport(Guid transportId, TransportForUpdateDto transport)
    {
        if (transport == null)
        {
            _logger.LogError($"Transport object sent from client is null.");
            throw new ArgumentNullException("Transport is null");
        }

        if (!await TransportExists(transportId))
        {
            _logger.LogError($"Transport with id: {transportId}, hasn't been found in db.");
            throw new ArgumentNullException("Invalid transport Id");
        }

        var transportEntity = await GetTransport(transportId);

        if (transport.ImgUrl is not null)
        {
            await _manageImage.UploadFileAsync(transport.ImgUrl);
            _manageImage.DeleteFile(transportEntity.ImgUrl);
        }
        else
        {
            _logger.LogError($"Img is null");
            throw new ArgumentException("ImgUrl cannot be null.");
        }



        _mapper.Map(transport, transportEntity);

        await _unitOfWorkRep.Transport.UpdateTransport(transportEntity);

        await _unitOfWorkRep.Save();
    }

    public async Task<Transport> CreateTransport(Guid categoryId, TransportForCreateDto transport)
    {

        if (categoryId == Guid.Empty || transport == null)
        {
            _logger.LogError("Error");
            throw new ArgumentNullException("Invalid categoryId or transport object.");
        }

        var categoryEntity = await _unitOfWorkRep.Category.GetCategory(categoryId);

        var transportMap = _mapper.Map<Transport>(transport);
        transportMap.TransportCategoryId = categoryEntity.Id;
        transportMap.ImgUrl = await _manageImage.UploadFileAsync(transport.ImgUrl); ;
        transportMap.CreatedUpdatedAt = DateTime.UtcNow;

        await _unitOfWorkRep.Transport.CreateTransport(transportMap);

        await _unitOfWorkRep.Save();

        return transportMap;
    }



}