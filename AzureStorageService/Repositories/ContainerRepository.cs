using Azure.Core.Pipeline;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

namespace AzureStorageService.Repositories;

public class ContainerRepository(IConfiguration configuration) : IContainerRepository
{
    private readonly BlobServiceClient _blobServiceClient = new(
        configuration.GetConnectionString("AzureStorage")!, new BlobClientOptions
        {
            Transport = new HttpClientTransport(new HttpClient
                { Timeout = Timeout.InfiniteTimeSpan }),
            Retry = { NetworkTimeout = Timeout.InfiniteTimeSpan }
        });
    
    public async Task<Result> CreateAsync(string containerName) =>
        (await (await _blobServiceClient.CreateBlobContainerAsync(containerName)).Value
            .ExistsAsync()).Value
            ? Result.Success()
            : Result.Failure();
    
    public async Task<Result> DeleteAsync(string containerName) =>
        !(await _blobServiceClient.DeleteBlobContainerAsync(containerName)).IsError
            ? Result.Success()
            : Result.Failure();
}