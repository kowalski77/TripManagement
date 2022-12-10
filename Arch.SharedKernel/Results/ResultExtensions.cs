namespace Arch.SharedKernel.Results;

public static class ResultExtensions
{
    public static async Task<Result<T>> Do<T>(this Result result, Func<Task<Result<T>>> func) =>
        result.NonNull().Success ?
            await func().NonNull().ConfigureAwait(false)
            : result.Error!;

    public static async Task<Result<TR>> Do<T, TR>(this Task<Result<T>> result, Func<T, Result<TR>> mapper)
    {
        Result<T> awaitedResult = await result.NonNull().ConfigureAwait(false);

        return awaitedResult.Success ?
            mapper.NonNull()(awaitedResult.Value) :
            awaitedResult.Error!;
    }

    public static async Task<Result<TR>> Do<T, TR>(this Task<Result<T>> result, Func<T, Task<TR>> func)
    {
        Result<T> awaitedResult = await result.NonNull().ConfigureAwait(false);
        return awaitedResult.Success ?
            await func(awaitedResult.Value) :
            awaitedResult.Error!;
    }

    public static Result Validate(this Result _, params Result[] results)
    {
        List<ErrorResult> errorCollection = (from result in results
                                             where result.Failure
                                             select result.Error!).ToList();

        return errorCollection.Any() ?
            errorCollection.First()! :  //TODO: rethink this, handle error collections in result & envelope
            Result.Ok();
    }

    public static Result<TR> Map<T, TR>(this Result<T> result, Func<T, TR> mapper) =>
    result.Success ?
            mapper.NonNull()(result.Value) :
            result.Error!;

}
