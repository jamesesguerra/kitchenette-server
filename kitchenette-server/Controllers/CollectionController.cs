using kitchenette_server.Interfaces.Collections;
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
}