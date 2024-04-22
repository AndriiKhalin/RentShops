using AutoMapper;
using Interfaces.IEntityService;
using Interfaces.IImageService;
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

    public TransportService(IUnitOfWorkRepository unitOfWorkRep, IMapper mapper, IManageImage<Transport> manageImage)
    {
        _unitOfWorkRep = unitOfWorkRep;
        _mapper = mapper;
        _manageImage = manageImage;
    }

    public async Task<List<Transport>> GetTransports()
    {
        return await _unitOfWorkRep.Transport.GetTransports();
    }

    public async Task<Transport?> GetTransport(Guid id)
    {
        return await _unitOfWorkRep.Transport.GetTransport(id);
    }

    public async Task<TransportCategory?> GetCategoryByTransport(Guid transportId)
    {
        return await _unitOfWorkRep.Transport.GetCategoryByTransport(transportId);
    }

    public async Task<IEnumerable<Order>> GetOrdersByTransport(Guid transportId)
    {
        return await _unitOfWorkRep.Transport.GetOrdersByTransport(transportId);
    }

    public async Task<bool> TransportExists(Guid id)
    {
        return await _unitOfWorkRep.Transport.TransportExists(id);
    }

    public void DeleteTransport(Guid id)
    {
        _unitOfWorkRep.Transport.DeleteTransport(id);
    }

    public async Task UpdateTransport(Guid transportId, TransportForUpdateDto transport)
    {
        var transportEntity = await _unitOfWorkRep.Transport.GetTransport(transportId);

        if (transportEntity is not null)
        {
            await _manageImage.UploadFileAsync(transport.ImgUrl);
        }

        _manageImage.DeleteFile(transportEntity.ImgUrl);

        _mapper.Map(transport, transportEntity);

        await _unitOfWorkRep.Transport.UpdateTransport(transportEntity);
    }

    public async Task<Transport> CreateTransport(Guid categoryId, TransportForCreateDto transport)
    {
        var categoryEntity = await _unitOfWorkRep.Category.GetCategory(categoryId);

        var imagePath = await _manageImage.UploadFileAsync(transport.ImgUrl);

        var transportMap = _mapper.Map<Transport>(transport);
        transportMap.TransportCategoryId = categoryEntity.Id;
        transportMap.ImgUrl = imagePath;
        transportMap.CreatedUpdatedAt = DateTime.UtcNow;

        await _unitOfWorkRep.Transport.CreateTransport(transportMap);

        return transportMap;
    }
}