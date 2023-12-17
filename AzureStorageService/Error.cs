using System.Text.Json.Serialization;

namespace AzureStorageService;

public class Error(string errorCode, string description, int statusCode = default)
{
    public string ErrorCode { get; } = errorCode;
    public string Description { get; } = description;
    [JsonIgnore] public int StatusCode { get; } = statusCode;
}