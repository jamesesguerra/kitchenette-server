using kitchenette_server.Interfaces.Collections;
using kitchenette_server.Models;

namespace kitchenette_server.Services;

public class CollectionService : ICollectionService
{
    private readonly ICollectionRepository _collectionRepository;
    
    public CollectionService(ICollectionRepository collectionRepository)
    {
        _collectionRepository = collectionRepository;
    }

    public async Task<IEnumerable<Collection>> GetCollectionsByUserId(string userId)
    {
        return await _collectionRepository.GetCollectionsByUserId(userId);
    }
}