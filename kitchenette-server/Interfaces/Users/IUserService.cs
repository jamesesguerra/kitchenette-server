using kitchenette_server.Models;

namespace kitchenette_server.Interfaces.Users;

public interface IUserService
{
    Task<User> GetUsersAsync(string id);
}