using Azure.Storage.Blobs.Models;
using AzureStorageService.DTOs;

namespace AzureStorageService.Repositories;

public interface IBlobRepository
{
    Task<Result<string>> UriAsync(BlobDto blobDto);
    Task<Result> DeleteAsync(BlobDto blobDto);
    Task<Result<BlobDownloadInfo>> DownloadAsync(BlobDto blobDto);
    Task<Result<string>> UploadAsync(UploadBlobDto uploadBlobDto);
}