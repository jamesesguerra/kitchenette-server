using kitchenette_server.Models;

namespace kitchenette_server.Interfaces.Collections;

public interface ICollectionService
{
    Task<IEnumerable<Collection>> GetCollectionsByUserId(string userId);
}