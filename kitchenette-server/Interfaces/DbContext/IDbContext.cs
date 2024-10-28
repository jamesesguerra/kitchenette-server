using System.Data;

namespace kitchenette_server.Interfaces.DbContext;

public interface IDbContext
{
    IDbConnection CreateConnection();
}