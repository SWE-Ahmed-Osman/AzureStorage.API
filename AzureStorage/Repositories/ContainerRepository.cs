using Azure.Storage.Blobs;
using AzureStorage.DTOs;

namespace AzureStorage.Repositories;

public class ContainerRepository(BlobServiceClient blobServiceClient) : IContainerRepository
{
    public async Task<Result<List<string>>> BlobsAsync(DirectoryDto directoryDto)
    {
        var blobs = new List<string>();

        var blobContainerClient = blobServiceClient.GetBlobContainerClient(directoryDto.Container);

        await foreach (var blobItem in blobContainerClient.GetBlobsAsync(
                           prefix: directoryDto.Directory))
            blobs.Add(blobItem.Name);

        return Result<List<string>>.Success(blobs);
    }

    public async Task<Result> CreateAsync(string containerName) =>
        (await (await blobServiceClient.CreateBlobContainerAsync(containerName)).Value
            .ExistsAsync()).Value
            ? Result.Success()
            : Result.Failure();

    public async Task<Result> DeleteAsync(string containerName) =>
        !(await blobServiceClient.DeleteBlobContainerAsync(containerName)).IsError
            ? Result.Success()
            : Result.Failure();

    public async Task<Result<long>> DirectorySizeAsync(DirectoryDto directoryDto)
    {
        var directorySize = 0L;
        var blobContainerClient = blobServiceClient.GetBlobContainerClient(directoryDto.Container);

        await foreach (var blobItem in blobContainerClient.GetBlobsAsync(
                           prefix: directoryDto.Directory))
            directorySize += (await blobContainerClient.GetBlobClient(blobItem.Name)
                .GetPropertiesAsync()).Value.ContentLength;

        return Result<long>.Success(directorySize);
    }
}