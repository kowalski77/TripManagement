namespace Arch.SharedKernel;

public static class MaybeExtensions
{
    public static Maybe<TResult> Map<T, TResult>(this Maybe<T> obj, Func<T, TResult> map) =>
        obj is Some<T> some ? new Some<TResult>(map.NonNull()(some.Content)) : new None<TResult>();

    public static Maybe<T> Filter<T>(this Maybe<T> obj, Func<T, bool> predicate) =>
        obj is Some<T> some && !predicate.NonNull()(some.Content) ? new None<T>() : obj;

    public static T Reduce<T>(this Maybe<T> obj, T substitute) =>
        obj is Some<T> some ? some.Content : substitute.NonNull();

    public static T Reduce<T>(this Maybe<T> obj, Func<T> substitute) =>
        obj is Some<T> some ? some.Content : substitute.NonNull()();

    public static TR Match<T, TR>(this Maybe<T> obj, Func<T, TR> some, Func<TR> none) =>
        obj is Some<T> someObj ? some.NonNull()(someObj.Content) : none.NonNull()();
}