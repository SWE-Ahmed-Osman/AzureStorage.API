using System.ComponentModel.DataAnnotations;

namespace AzureStorage.DTOs;

public class DirectoryDto
{
    [Required] public string Directory { get; init; } = null!;
    [Required] public string Container { get; init; } = null!;
}