using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using kitchenette_server.Models;
using Microsoft.AspNetCore.Mvc;

namespace kitchenette_server.Controllers;

[ApiController]
[Route("api/files")]
public class FileController : ControllerBase
{
    private readonly BlobServiceClient _blobService;
    private readonly BlobContainerClient _blobContainer;
    
    public FileController(BlobServiceClient blobService)
    {
        _blobService = blobService;
        _blobContainer = blobService.GetBlobContainerClient("kitchenet");
    }

    [HttpPost("")]
    public async Task<IActionResult> UploadFile(IFormFile blob)
    {
        var blobClient = _blobContainer.GetBlobClient(blob.FileName);
        var contentType = blob.ContentType;
        var blobHttpHeaders = new BlobHttpHeaders() { ContentType = contentType };
        await using var fileStream = blob.OpenReadStream();
        await blobClient.UploadAsync(fileStream, blobHttpHeaders);
        return Ok(new { Uri = blobClient.Uri.AbsoluteUri });
    }

    [HttpDelete("")]
    public async Task<IActionResult> DeleteFile(string blobName)
    {
        var blobClient = _blobContainer.GetBlobClient(blobName);
        await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
        return NoContent();
    }
}