using Entities;
using Entities.Models;
using Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly RentDbContext _context;

    public UserRepository(RentDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        return await GetAll();
    }

    public async Task<User> GetUser(Guid id)
    {
        return await GetByCondition(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<User> GetUser(string username)
    {
        return await GetByCondition(x => x.FirstName == username).FirstOrDefaultAsync();
    }

    public async Task<DateTime?> GetLastUserOrder(Guid id)
    {
        var user = await GetByCondition(x => x.Id == id).Include(x => x.Orders).FirstOrDefaultAsync();

        if (user != null && user.Orders.Any())
        {
            var lastOrder = user.Orders.OrderByDescending(x => x.DateTo).FirstOrDefault();

            return lastOrder.DateTo;
        }

        return null;
    }

    public async Task CreateUser(User user)
    {
        await Create(user);
    }

    public void UpdateUser(User user)
    {
        Update(user);

    }

    public void DeleteUser(Guid id)
    {
        Delete(id);
    }

    public async Task<bool> UserExists(Guid id)
    {
        return await Exists(id);
    }
}