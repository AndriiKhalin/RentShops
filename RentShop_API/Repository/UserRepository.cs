using RentShop_API.Interfaces;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace RentShop_API.Repository;

public class UserRepository : IUserRepository
{
    private readonly RentDbContext _context;

    public UserRepository(RentDbContext context)
    {
        _context = context;
    }

    public async Task<User> CreateUser(User user)
    {
        var result = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    //public async Task<bool> CreateUser(User user)
    //{
    //    await _context.Users.AddAsync(user);
    //    return await Save();
    //}

    public async Task<DateTime?> GetLastUserOrder(Guid id)
    {
        var user = await _context.Users.Include(x => x.Orders).FirstOrDefaultAsync(x => x.Id == id);

        if (user != null && user.Orders.Any())
        {
            var lastOrder = user.Orders.OrderByDescending(x => x.DateTo).FirstOrDefault();

            return lastOrder.DateTo;
        }

        return null;
    }

    public async Task<User> GetUser(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User> GetUser(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Name == username);
    }

    public async Task<List<User>> GetUsers()
    {

        return await _context.Users.ToListAsync();
    }

    //public async Task<bool> Save()
    //{
    //    var saved = await _context.SaveChangesAsync();
    //    return saved > 0 ? true : false;
    //}

    public async Task<bool> UserExists(Guid id)
    {
        return await _context.Users.AnyAsync(x => x.Id == id);
    }
}