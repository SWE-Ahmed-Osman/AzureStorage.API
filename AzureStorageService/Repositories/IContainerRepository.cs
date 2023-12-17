namespace AzureStorageService.Repositories;

public interface IContainerRepository
{
    Task<Result> CreateAsync(string containerName);
    Task<Result> DeleteAsync(string containerName);
}