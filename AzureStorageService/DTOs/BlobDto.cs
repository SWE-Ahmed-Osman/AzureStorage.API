using System.ComponentModel.DataAnnotations;

namespace AzureStorageService.DTOs;

public class BlobDto
{
    [Required] public string Blob { get; init; } = null!;
    [Required] public string Container { get; init; } = null!;
}