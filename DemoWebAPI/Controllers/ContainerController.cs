using AzureStorageService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ContainerController(IContainerRepository containerRepository) : ControllerBase
{
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
}