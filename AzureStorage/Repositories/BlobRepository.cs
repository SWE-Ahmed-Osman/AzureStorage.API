using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using AzureStorage.DTOs;

namespace AzureStorage.Repositories;

public class BlobRepository(BlobServiceClient blobServiceClient) : IBlobRepository
{
    public async Task<Result<string>> UriAsync(BlobDto blobDto)
    {
        var blobClient = blobServiceClient.GetBlobContainerClient(blobDto.Container)
            .GetBlobClient(blobDto.Blob);

        return (await blobClient.ExistsAsync()).Value
            ? Result<string>.Success(blobClient
                .GenerateSasUri(BlobSasPermissions.Read, DateTimeOffset.UtcNow.AddMinutes(1))
                .AbsoluteUri)
            : Result<string>.Failure();
    }

    public async Task<Result> DeleteAsync(BlobDto blobDto)
    {
        var blobClient = blobServiceClient.GetBlobContainerClient(blobDto.Container)
            .GetBlobClient(blobDto.Blob);
        return (await blobClient.DeleteIfExistsAsync()).Value
            ? Result.Success()
            : Result.Failure();
    }

    public async Task<Result<BlobDownloadInfo>> DownloadAsync(BlobDto blobDto)
    {
        var blobClient = blobServiceClient.GetBlobContainerClient(blobDto.Container)
            .GetBlobClient(blobDto.Blob);
        return (await blobClient.ExistsAsync()).Value
            ? Result<BlobDownloadInfo>.Success((await blobClient.DownloadAsync()).Value)
            : Result<BlobDownloadInfo>.Failure();
    }

    public async Task<Result<string>> UploadAsync(UploadBlobDto uploadBlobDto)
    {
        var blobClient = blobServiceClient.GetBlobContainerClient(uploadBlobDto.Container)
            .GetBlobClient(Guid.NewGuid() + Path.GetExtension(uploadBlobDto.Blob.FileName));

        var blobContentInfo
            = await blobClient.UploadAsync(uploadBlobDto.Blob.OpenReadStream(),
                new BlobUploadOptions
                {
                    TransferOptions = new()
                    {
                        MaximumConcurrency = 8,
                        InitialTransferSize = 1024 * 1024,
                        MaximumTransferSize = long.MaxValue
                    }
                });

        return blobContentInfo is not null
            ? Result<string>.Success(blobClient
                .GenerateSasUri(BlobSasPermissions.Read, DateTimeOffset.UtcNow.AddMinutes(1))
                .AbsoluteUri)
            : Result<string>.Failure();
    }
}