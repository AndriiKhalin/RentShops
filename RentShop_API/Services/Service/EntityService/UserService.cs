using AutoMapper;
using Interfaces.IEntityService;
using Interfaces.IImageService;
using Interfaces.ILoggerService;
using Interfaces.IRepository;
using Models.DTO.UserDTO;
using Models.Entities;
using System.Security.Cryptography.Xml;

namespace Services.Service.EntityService;

public class UserService : IUserService
{

    private readonly IUnitOfWorkRepository _unitOfWorkRep;
    private readonly IMapper _mapper;
    private readonly IManageImage<User> _manageImage;
    private readonly ILoggerManager _logger;

    public UserService(IUnitOfWorkRepository unitOfWorkRep, IMapper mapper, IManageImage<User> manageImage, ILoggerManager logger)
    {
        _unitOfWorkRep = unitOfWorkRep;
        _mapper = mapper;
        _manageImage = manageImage;
        _logger = logger;
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        return await _unitOfWorkRep.User.GetUsers();
    }

    public async Task<User> GetUser(Guid id)
    {
        return await _unitOfWorkRep.User.GetUser(id);
    }

    public async Task<User> GetUser(string username)
    {
        return await _unitOfWorkRep.User.GetUser(username);
    }

    public async Task<IEnumerable<Rating>> GetRatingsByUser(Guid userId)
    {
        return await _unitOfWorkRep.User.GetRatingsByUser(userId);
    }

    public async Task<DateTime?> GetLastUserOrder(Guid id)
    {
        return await _unitOfWorkRep.User.GetLastUserOrder(id);
    }

    public async Task<User> CreateUser(UserForCreateDto user)
    {
        if (user == null)
        {
            _logger.LogError("User object is null");
            throw new ArgumentNullException("User object is null.");
        }

        var userMap = _mapper.Map<User>(user);
        userMap.ImgUrl = await _manageImage.UploadFileAsync(user.ImgUrl);
        userMap.CreatedUpdatedAt = DateTime.Now;

        await _unitOfWorkRep.User.CreateUser(userMap);

        await _unitOfWorkRep.Save();

        return userMap;
    }

    public async Task UpdateUser(Guid userId, UserForUpdateDto user)
    {
        if (user == null)
        {
            _logger.LogError($"User object sent from client is null.");
            throw new ArgumentNullException("User object is null.");
        }

        if (!await UserExists(userId))
        {
            _logger.LogError($"User with id: {userId}, hasn't been found in db.");
            throw new ArgumentNullException($"User with id: {userId}, hasn't been found in db.");
        }

        var userEntity = await GetUser(userId);

        if (user.ImgUrl is not null)
        {
            await _manageImage.UploadFileAsync(user.ImgUrl);
            _manageImage.DeleteFile(userEntity.ImgUrl);
        }
        else
        {
            _logger.LogError($"Img is null");
            throw new ArgumentException("ImgUrl cannot be null.");
        }

        _mapper.Map(user, userEntity);


        await _unitOfWorkRep.User.UpdateUser(userEntity);

        await _unitOfWorkRep.Save();
    }

    public async Task DeleteUser(Guid userId)
    {
        if (!await UserExists(userId))
        {
            _logger.LogError($"User with id: {userId}, hasn't been found in db.");
            throw new ArgumentNullException($"Invalid user Id: {userId}");
        }

        var userEntityForDelete = await GetUser(userId);

        _manageImage.DeleteFile(userEntityForDelete.ImgUrl);

        _unitOfWorkRep.User.DeleteUser(userId);

        await _unitOfWorkRep.Save();

    }

    public async Task<bool> UserExists(Guid id)
    {
        return await _unitOfWorkRep.User.UserExists(id);
    }

    public async Task<bool> UserExists(string userName)
    {
        return await _unitOfWorkRep.User.UserExists(userName);
    }
}