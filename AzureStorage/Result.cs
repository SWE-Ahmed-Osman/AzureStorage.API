namespace AzureStorage;

public class Error(int statusCode, string description)
{
    public int StatusCode { get; } = statusCode;
    public string Description { get; } = description;
}

public class Result(bool succeeded, IEnumerable<Error>? errors)
{
    public bool Succeeded { get; } = succeeded;
    public IEnumerable<Error>? Errors { get; } = errors;

    public static Result Success() =>
        new Result(true, null);
    public static Result Failure(IEnumerable<Error>? errors = default) =>
        new Result(false, errors);
}

public class Result<T>(bool succeeded, IEnumerable<Error>? errors, T? data) : Result(succeeded, errors)
{
    public T? Data { get; } = data;

    public static Result<T> Success(T? data = default) =>
        new Result<T>(true, null, data);

    public static Result<T> Failure(IEnumerable<Error>? errors = default, T? data = default) =>
        new Result<T>(false, errors, data);
}