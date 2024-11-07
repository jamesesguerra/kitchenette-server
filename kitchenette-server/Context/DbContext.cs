using System.Data;
using kitchenette_server.Interfaces.DbContext;
using Microsoft.Data.SqlClient;

namespace kitchenette_server.Context;

public class DbContext : IDbContext
{
     private readonly string _connectionString;

    public DbContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
}