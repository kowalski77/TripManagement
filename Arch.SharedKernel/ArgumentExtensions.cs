using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Arch.SharedKernel;

public static class ArgumentExtensions
{
    public static T NonNull<T>([NotNull] this T value, [CallerArgumentExpression("value")] string? paramName = null) =>
        value is not null ?
        value :
        throw new ArgumentNullException(paramName);

    public static string NonNullOrEmpty([NotNull] this string value, [CallerArgumentExpression("value")] string? paramName = null) =>
        !string.IsNullOrEmpty(value) ?
        value :
        throw new ArgumentNullException(paramName);

    public static int EnsureRange(this int value, int min, int max, [CallerArgumentExpression("value")] string? paramName = null) =>
        value >= min && value <= max ?
        value :
        throw new ArgumentOutOfRangeException(paramName, $"Value must be between {min} and {max}.");
}
