using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace AzureStorage.DTOs;

public class UploadBlobDto
{
    [Required] public IFormFile Blob { get; init; } = null!;
    [Required] public string Container { get; init; } = null!;
}