using System.Data;

namespace kitchenette_server.Interfaces;

public interface IDbContext
{
    IDbConnection CreateConnection();
}