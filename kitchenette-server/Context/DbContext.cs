using System.Data;
using kitchenette_server.Interfaces.DbContext;
using Npgsql;

namespace kitchenette_server.Context;

public class DbContext : IDbContext
{
     private readonly string _connectionString;

    public DbContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
}