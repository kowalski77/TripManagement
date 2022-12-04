namespace Arch.SharedKernel.Results;

public static class ResultExtensions
{
    public static Result Do(this Result _, Func<Result> func) => func().NonNull();

    public static Result<T> Do<T>(this Result _, Func<Result<T>> func) => func().NonNull();

    public static async Task<Result<T>> Do<T>(this Result _, Func<Task<Result<T>>> func) => await func().NonNull().ConfigureAwait(false);

    public static async Task<Result> OnSuccess(this Result result, Func<Task<Result>> func) =>
        result.NonNull().Success ?
            await func().NonNull().ConfigureAwait(false)
            : result.Error!;

    public static async Task<Result<(T, TK)>> OnSuccess<T, TK>(this Task<Result> result, Func<Task<Result<(T, TK)>>> func)
    {
        Result awaitedResult = await result.NonNull().ConfigureAwait(false);

        return awaitedResult.Success ?
            await func().NonNull().ConfigureAwait(false) :
            awaitedResult.Error!;
    }

    public static async Task<Result<T>> OnSuccess<T>(this Result result, Func<Task<Result<T>>> func) =>
        result.NonNull().Success ?
            await func().NonNull().ConfigureAwait(false)
            : result.Error!;

    public static async Task<Result<TR>> OnSuccess<T, TR>(this Task<Result<T>> result, Func<T, TR> mapper)
    {
        Result<T> awaitedResult = await result.NonNull().ConfigureAwait(false);

        return awaitedResult.Success ?
            mapper.NonNull()(awaitedResult.Value) :
            awaitedResult.Error!;
    }

    public static async Task<Result> OnSuccess<T>(this Task<Result<T>> result, Func<T, Task<Result>> func)
    {
        Result<T> awaitedResult = await result.NonNull().ConfigureAwait(false);

        return awaitedResult.Success ?
            await func.NonNull()(awaitedResult.Value).ConfigureAwait(false) :
            awaitedResult.Error!;
    }
}
