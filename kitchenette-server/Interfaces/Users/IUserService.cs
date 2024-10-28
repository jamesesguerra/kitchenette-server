using kitchenette_server.Models;

namespace kitchenette_server.Interfaces.Users;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsersAsync();
}