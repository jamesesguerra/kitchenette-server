using kitchenette_server.Dtos;
using kitchenette_server.Interfaces.Collections;
using kitchenette_server.Models;
using Microsoft.AspNetCore.Mvc;

namespace kitchenette_server.Controllers;

[ApiController]
[Route("api/collections")]
public class CollectionController : ControllerBase
{
    private readonly ICollectionService _collectionService;
    
    public CollectionController(ICollectionService collectionService)
    {
        _collectionService = collectionService;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetCollectionsByUserId([FromQuery] string userId)
    {
        var collections = await _collectionService.GetCollectionsByUserId(userId);
        return Ok(collections);
    }

    [HttpPost("")]
    public async Task<IActionResult> AddCollection(Collection newCollection)
    {
        var collection = await _collectionService.AddCollection(newCollection);
        return Ok(collection);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCollection([FromRoute] int id)
    {
        var affectedRows = await _collectionService.DeleteCollection(id);

        if (affectedRows == 0)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("{id}/recipes")]
    public async Task<IActionResult> GetCollectionByIdWithRecipes([FromRoute] int id)
    {
        var collection = await _collectionService.GetCollectionByIdWithRecipes(id);
        return Ok(collection);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateCollection(int id, [FromBody] CollectionUpdateDto updatedCollection)
    {
        var collection = await _collectionService.GetCollectionById(id);
        if (collection is null) return NotFound();
        
        collection.Name = updatedCollection.Name ?? collection.Name;
        collection.Description = updatedCollection.Description ?? collection.Description;

        await _collectionService.UpdateCollection(id, collection);
        return NoContent();
    }
}