using System.ComponentModel.DataAnnotations;

namespace AzureStorageService.DTOs;

public class DirectoryDto
{
    [Required] public string Directory { get; init; } = null!;
    [Required] public string Container { get; init; } = null!;
}