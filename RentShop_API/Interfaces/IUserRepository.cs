using System.Runtime.InteropServices.JavaScript;

namespace RentShop_API.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetUsers();

    Task<User> GetUser(Guid id);

    Task<User> GetUser(string username);

    Task<DateTime?> GetLastUserOrder(Guid id);

    Task<bool> UserExists(Guid id);
}