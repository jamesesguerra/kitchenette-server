using kitchenette_server.Interfaces.DbContext;
using kitchenette_server.Interfaces.Users;
using kitchenette_server.Models;
using Dapper;

namespace kitchenette_server.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbContext _context;

    public UserRepository(IDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        using var connection = _context.CreateConnection();

        var sql = @" SELECT Id, 
                            Name,
                            Nickname,
                            Email,
                            Bio,
                            Picture,
                            CreatedAt
                     FROM Users ";
        var users = await connection.QueryAsync<User>(sql);
        return users;
    }
}