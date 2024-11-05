using kitchenette_server.Models;

namespace kitchenette_server.Interfaces.Users;

public interface IUserRepository
{
    Task<User> GetUserById(string id);
}