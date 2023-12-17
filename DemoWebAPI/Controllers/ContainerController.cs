using AzureStorageService.DTOs;
using AzureStorageService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ContainerController(IContainerRepository containerRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Blobs([FromBody] DirectoryDto directoryDto) =>
        Ok((await containerRepository.BlobsAsync(directoryDto)).Data);

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] string containerName) =>
        (await containerRepository.CreateAsync(containerName)).Succeeded
            ? Ok(containerName)
            : BadRequest();

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] string containerName) =>
        (await containerRepository.DeleteAsync(containerName)).Succeeded
            ? Ok()
            : NotFound(containerName);

    [HttpGet]
    public async Task<IActionResult> DirectorySize([FromBody] DirectoryDto directoryDto)
    {
        var directorySizeResult = await containerRepository.DirectorySizeAsync(directoryDto);
        return directorySizeResult.Succeeded
            ? Ok(directorySizeResult.Data)
            : NotFound();
    }
}