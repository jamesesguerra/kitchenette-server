using kitchenette_server.Dtos;
using kitchenette_server.Models;

namespace kitchenette_server.Interfaces.Collections;

public interface ICollectionService
{
    Task<IEnumerable<Collection>> GetCollectionsByUserId(string userId, bool? isVisible);
    Task<Collection?> GetCollectionById(int id);
    Task<Collection> AddCollection(Collection newCollection);
    Task<int> DeleteCollection(int id);
    Task<CollectionDto> GetCollectionByIdWithRecipes(int id);
    Task UpdateCollection(int id, Collection updatedCollection);
    Task<IEnumerable<CollectionDto>> GetRecentCollections();
}