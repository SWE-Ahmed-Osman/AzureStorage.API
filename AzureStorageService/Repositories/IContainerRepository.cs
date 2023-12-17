using AzureStorageService.DTOs;

namespace AzureStorageService.Repositories;

public interface IContainerRepository
{
    Task<Result<List<string>>> BlobsAsync(DirectoryDto directoryDto);
    Task<Result> CreateAsync(string containerName);
    Task<Result> DeleteAsync(string containerName);
    Task<Result<long>> DirectorySizeAsync(DirectoryDto directoryDto);
}