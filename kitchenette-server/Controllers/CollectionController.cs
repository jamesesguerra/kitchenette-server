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

    [HttpDelete("{collectionId}")]
    public async Task<IActionResult> DeleteCollection([FromRoute] int collectionId)
    {
        var affectedRows = await _collectionService.DeleteCollection(collectionId);

        if (affectedRows == 0)
        {
            return NotFound();
        }

        return NoContent();
    }
    
}