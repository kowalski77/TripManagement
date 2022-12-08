namespace Arch.SharedKernel;

public readonly struct Maybe<T> : IEquatable<Maybe<T>>
{
    private readonly T value = default!;

    private Maybe(T value)
    {
        this.value = value;
        HasValue = true;
    }

    public T Value => ValueOrThrow();

    public bool HasValue { get; }

    public bool HasNoValue => !HasValue;

    public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none)
    {
        ArgumentNullException.ThrowIfNull(some);
        ArgumentNullException.ThrowIfNull(none);

        return HasValue ? some(value) : none();
    }

    public static implicit operator Maybe<T>(T? value) =>
        value == null ? new Maybe<T>() : new Maybe<T>(value);

    public static implicit operator Maybe<T>(Maybe _) => None;

    public static Maybe<T> None => new();

    public Maybe<TResult> Map<TResult>(Func<T, TResult> convert)
    {
        ArgumentNullException.ThrowIfNull(convert);

        return !HasValue ? new Maybe<TResult>() : convert(value);
    }

    public Maybe<TResult> Bind<TResult>(Func<T, Maybe<TResult>> convert)
    {
        ArgumentNullException.ThrowIfNull(convert);

        return !HasValue ? new Maybe<TResult>() : convert(value);
    }

    public T ValueOrThrow(string? errorMessage = null) => HasValue ? value : throw new InvalidOperationException(errorMessage);

    public Maybe<T> ToMaybe() => throw new NotImplementedException();

    public bool Equals(Maybe<T> other) => HasValue == other.HasValue && EqualityComparer<T>.Default.Equals(value, other.value);

    public override bool Equals(object? obj) => obj is Maybe<T> other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(HasValue, value);

    public static bool operator ==(Maybe<T> left, Maybe<T> right) => left.Equals(right);

    public static bool operator !=(Maybe<T> left, Maybe<T> right) => !left.Equals(right);
}

public struct Maybe
{
    public static Maybe None => new();
}
