using kitchenette_server.Interfaces.DbContext;
using kitchenette_server.Models;
using Dapper;
using kitchenette_server.Interfaces.Collections;

namespace kitchenette_server.Repositories;

public class CollectionRepository : ICollectionRepository
{
    private readonly IDbContext _context;

    public CollectionRepository(IDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Collection>> GetCollectionsByUserId(string userId)
    {
        using var connection = _context.CreateConnection();

        var sql = " SELECT * FROM Collection WHERE UserId = @userId ";
        var collections = await connection.QueryAsync<Collection>(sql, new { userId });
        return collections;
    }
}