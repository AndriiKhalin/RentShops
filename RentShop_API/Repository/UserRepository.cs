
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using AutoMapper;
using Interfaces.IRepository;
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

    public async Task CreateUser(User user)
    {
        await Create(user);
    }

    public async Task UpdateUser(User userForUpdate)
    {
        Update(userForUpdate);
    }

    public void DeleteUser(Guid id)
    {
        Delete(id);
    }

    public async Task<bool> UserExists(Guid id)
    {
        return await Exists(x => x.Id == id);
    }

    public async Task<bool> UserExists(string userName)
    {

        return await Exists(x => x.FirstName == userName);
    }

}