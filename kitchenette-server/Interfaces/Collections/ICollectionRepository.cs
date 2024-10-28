using kitchenette_server.Models;

namespace kitchenette_server.Interfaces.Collections;

public interface ICollectionRepository
{
    Task<IEnumerable<Collection>> GetCollectionsByUserId(string userId);
}