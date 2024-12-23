using kitchenette_server.Dtos;
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

    public async Task<IEnumerable<Collection>> GetCollectionsByUserId(string userId, bool? isVisible)
    {
        return await _collectionRepository.GetCollectionsByUserId(userId, isVisible);
    }

    public async Task<Collection?> GetCollectionById(int id)
    {
        return await _collectionRepository.GetCollectionById(id);
    }

    public async Task<Collection> AddCollection(Collection newCollection)
    {
        return await _collectionRepository.AddCollection(newCollection);
    }

    public async Task<int> DeleteCollection(int id)
    {
        return await _collectionRepository.DeleteCollection(id);
    }

    public async Task<CollectionDto> GetCollectionByIdWithRecipes(int id)
    {
        return await _collectionRepository.GetCollectionByIdWithRecipes(id);
    }

    public async Task UpdateCollection(int id, Collection updatedCollection)
    {
        await _collectionRepository.UpdateCollection(id, updatedCollection);
    }

    public async Task<IEnumerable<CollectionDto>> GetRecentCollections()
    {
        return await _collectionRepository.GetRecentCollections();
    }
}