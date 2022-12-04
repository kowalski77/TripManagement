namespace Arch.SharedKernel.Results;

public sealed class Result<T> : Result
{
    private readonly T value;

    public Result(T value) => this.value = value;

    public Result(ErrorResult error) : base(error) => value = default!;

    public T Value => Failure ? throw new InvalidOperationException("The result object has no value.") : value;

    public static implicit operator Result<T>(ErrorResult errorResult) => new(errorResult);

    public static implicit operator Result<T>(T value) => new(value);
}
