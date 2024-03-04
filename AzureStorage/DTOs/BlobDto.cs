using System.ComponentModel.DataAnnotations;

namespace AzureStorage.DTOs;

public class BlobDto
{
    [Required] public string Blob { get; init; } = null!;
    [Required] public string Container { get; init; } = null!;
}