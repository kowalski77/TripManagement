namespace Arch.SharedKernel;

public abstract class Maybe<T>
{
    public static implicit operator Maybe<T>(None _) => new None<T>();
    public static implicit operator Maybe<T>(T value) => value is null ? new None<T>() : new Some<T>(value);

    public Maybe<T> ToMaybe() => throw new NotImplementedException();
}

public sealed class Some<T> : Maybe<T>
{
    public T Content { get; }

    public Some(T content) => Content = content;

    public override string ToString() => $"Some {Content?.ToString() ?? "<null>"}";
}

public sealed class None<T> : Maybe<T>
{
    public override string ToString() => $"None";
}

public sealed class None
{
    public static None Value { get; } = new();

    public static None<T> Of<T>() => new();
}