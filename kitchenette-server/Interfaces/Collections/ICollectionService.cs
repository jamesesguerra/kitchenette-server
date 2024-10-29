using kitchenette_server.Models;

namespace kitchenette_server.Interfaces.Collections;

public interface ICollectionService
{
    Task<IEnumerable<Collection>> GetCollectionsByUserId(string userId);
    Task<Collection> AddCollection(Collection newCollection);
    Task<int> DeleteCollection(int id);
}