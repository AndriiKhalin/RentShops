using Services.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using AutoMapper;
using Microsoft.Extensions.FileProviders;
using Models;
using Models.DTO.UserDTO;
using Models.Entities;

namespace Repository;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly RentDbContext _context;
    private readonly IFileProvider _fileProvider;
    private readonly IMapper _mapper;

    public UserRepository(RentDbContext context, IFileProvider fileProvider, IMapper mapper) : base(context)
    {
        _context = context;
        _fileProvider = fileProvider;
        _mapper = mapper;
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        return await GetAll().Result.OrderBy(x => x.CreatedUpdatedAt).ToListAsync();
    }

    public async Task<User> GetUser(Guid id)
    {
        return await GetByCondition(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<User> GetUser(string username)
    {
        return await GetByCondition(x => x.FirstName == username).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Rating>> GetRatingsByUser(Guid userId)
    {
        return await GetByCondition(x => x.Id == userId).Include(x => x.Ratings).SelectMany(x => x.Ratings)
            .ToListAsync();
    }

    public async Task<DateTime?> GetLastUserOrder(Guid id)
    {
        var user = await GetByCondition(x => x.Id == id).Include(x => x.Orders).FirstOrDefaultAsync();

        if (user != null && user.Orders.Any())
        {
            var lastOrder = user.Orders.OrderByDescending(x => x.OrderDateTo).FirstOrDefault();

            return lastOrder.OrderDateTo;
        }

        return null;
    }

    public async Task<User> CreateUser(UserForCreateDto user)
    {
        var src = "";
        string rootImg = "/Upload/User/";
        var username = $"{user.FirstName}_{user.LastName}({Guid.NewGuid()}){Path.GetExtension(user.ImgUrl.FileName)}";

        if (user.ImgUrl is not null)
        {
            var root = @"D:\IT\My_Projects\RentShop\RentShop_UI\Stuff\Images\Upload\User\";

            var directoryPath = Path.GetDirectoryName(root);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            src = Path.Combine(root, username);

            using (var fileStream = new FileStream(src, FileMode.Create))
            {
                await user.ImgUrl.CopyToAsync(fileStream);
            }
        }

        var userMap = _mapper.Map<User>(user);
        userMap.ImgUrl = rootImg + username;
        userMap.CreatedUpdatedAt = DateTime.Now;

        await Create(userMap);

        return userMap;
    }

    public async Task UpdateUser(Guid userId, UserForUpdateDto user)
    {
        var userEntity = await GetByCondition(x => x.Id == userId).FirstOrDefaultAsync();
        var src = "";
        var root = @"D:\IT\My_Projects\RentShop\RentShop_UI\Stuff\Images\Upload\User\";
        string rootImg = "/Upload/User/";
        var username = $"{user.FirstName}_{user.LastName}({Guid.NewGuid()}){Path.GetExtension(user.ImgUrl.FileName)}";

        if (userEntity is not null)
        {

            if (user.ImgUrl is not null)
            {

                var directoryPath = Path.GetDirectoryName(root);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                src = Path.Combine(root, username);

                using (var fileStream = new FileStream(src, FileMode.Create))
                {
                    await user.ImgUrl.CopyToAsync(fileStream);
                }
            }

            if (!string.IsNullOrEmpty(userEntity.ImgUrl))
            {
                string oldsrc = userEntity.ImgUrl;
                System.IO.File.Delete(oldsrc);
            }


            var firstName = userEntity.FirstName;
            var secondName = userEntity.LastName;
            var birthDate = userEntity.BirthDate;
            var email = userEntity.Email;
            var phone = userEntity.Phone;
            var password = userEntity.Password;

            _mapper.Map(user, userEntity);

            userEntity.FirstName = firstName;
            userEntity.LastName = secondName;
            userEntity.BirthDate = birthDate;
            userEntity.Email = email;
            userEntity.Phone = phone;
            userEntity.Password = password;
            userEntity.ImgUrl = rootImg + username;
            userEntity.CreatedUpdatedAt = DateTime.Now;

            Update(userEntity);

        }
    }

    public void DeleteUser(Guid id)
    {
        Delete(id);
    }

    public async Task<bool> UserExists(Guid id)
    {
        return await Exists(id);
    }

    public async Task<bool> UserExists(string userName)
    {

        return await Exists(userName);
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