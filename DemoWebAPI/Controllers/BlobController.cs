using AzureStorageService.DTOs;
using AzureStorageService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class BlobController(IBlobRepository blobRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Uri([FromBody] BlobDto blobDto)
    {
        var blobUriResult = await blobRepository.UriAsync(blobDto);
        return blobUriResult.Succeeded
            ? Ok(blobUriResult.Data)
            : NotFound(blobDto);
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] BlobDto blobDto) =>
        (await blobRepository.DeleteAsync(blobDto)).Succeeded
            ? Ok()
            : NotFound(blobDto);
    
    [HttpGet]
    public async Task<IActionResult> Download([FromBody] BlobDto blobDto)
    {
        var downloadBlobResult = await blobRepository.DownloadAsync(blobDto);
        return downloadBlobResult.Succeeded
            ? Ok(downloadBlobResult.Data)
            : NotFound(blobDto);
    }
    
    [HttpPost]
    [RequestSizeLimit(long.MaxValue)]
    public async Task<IActionResult> Upload([FromForm] UploadBlobDto uploadBlobDto)
    {
        var blobUriResult = await blobRepository.UploadAsync(uploadBlobDto);
        return blobUriResult.Succeeded
            ? Ok(blobUriResult.Data)
            : BadRequest(uploadBlobDto);
    }
}