namespace Arch.SharedKernel;

public readonly struct Maybe<T> : IEquatable<Maybe<T>>
{
    private readonly T value = default!;

    private Maybe(T value)
    {
        this.value = value;
        this.HasValue = true;
    }

    public T Value => this.ValueOrThrow();

    public bool HasValue { get; }

    public bool HasNoValue => !this.HasValue;

    public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none)
    {
        ArgumentNullException.ThrowIfNull(some);
        ArgumentNullException.ThrowIfNull(none);

        return this.HasValue ? some(this.value) : none();
    }

    public static implicit operator Maybe<T>(T? value)
    {
        return value == null ? new Maybe<T>() : new Maybe<T>(value);
    }

    public static implicit operator Maybe<T>(Maybe value)
    {
        return None;
    }

    public static Maybe<T> None => new();

    public Maybe<TResult> Map<TResult>(Func<T, TResult> convert)
    {
        ArgumentNullException.ThrowIfNull(convert);

        return !this.HasValue ? new Maybe<TResult>() : convert(this.value);
    }

    public Maybe<TResult> Bind<TResult>(Func<T, Maybe<TResult>> convert)
    {
        ArgumentNullException.ThrowIfNull(convert);

        return !this.HasValue ? new Maybe<TResult>() : convert(this.value);
    }

    public T ValueOrThrow(string? errorMessage = null)
    {
        if (this.HasValue)
        {
            return this.value;
        }

        throw new InvalidOperationException(errorMessage);
    }

    public Maybe<T> ToMaybe()
    {
        throw new NotImplementedException();
    }

    public bool Equals(Maybe<T> other)
    {
        return this.HasValue == other.HasValue && EqualityComparer<T>.Default.Equals(this.value, other.value);
    }

    public override bool Equals(object? obj)
    {
        return obj is Maybe<T> other && this.Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.HasValue, this.value);
    }

    public static bool operator ==(Maybe<T> left, Maybe<T> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Maybe<T> left, Maybe<T> right)
    {
        return !left.Equals(right);
    }
}

public struct Maybe
{
    public static Maybe None => new();
}
