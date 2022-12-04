namespace Arch.SharedKernel.Results;

public class Result
{
    protected Result(ErrorResult error)
    {
        Success = false;
        Error = error;
    }

    protected Result() => Success = true;

    public ErrorResult? Error { get; }

    public bool Success { get; }

    public bool Failure => !Success;

    public static Result Init => Ok();

    public static Result Ok() => new();

    public static Result Fail(ErrorResult error) => new(error);

    public static implicit operator Result(ErrorResult error) => new(error);

    public static Result<T> Ok<T>(T value) => new(value);

    public static Result<T> Fail<T>(ErrorResult error) => new(error);

    public Result ToResult() => throw new NotImplementedException();
}
