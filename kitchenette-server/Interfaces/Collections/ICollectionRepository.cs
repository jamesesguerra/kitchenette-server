using kitchenette_server.Dtos;
using kitchenette_server.Models;

namespace kitchenette_server.Interfaces.Collections;

public interface ICollectionRepository
{
    Task<IEnumerable<Collection>> GetCollectionsByUserId(string userId);
    Task<Collection?> GetCollectionById(int id);
    Task<Collection> AddCollection(Collection newCollection);
    Task<int> DeleteCollection(int collectionId);
    Task<CollectionDto> GetCollectionByIdWithRecipes(int id);
    Task UpdateCollection(int id, Collection updatedCollection);
}