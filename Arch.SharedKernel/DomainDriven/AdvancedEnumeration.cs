#pragma warning disable CS8618
using System.Reflection;

namespace Arch.SharedKernel.DomainDriven;

public class AdvancedEnumeration<T>
{
    protected AdvancedEnumeration() { }

    protected AdvancedEnumeration(int id, string name) => (Id, Name) = (id, name);

    public int Id { get; }

    public string Name { get; }

    protected static IEnumerable<T> All => LazyEnumeration.Value;

    private static readonly Lazy<IEnumerable<T>> LazyEnumeration = new(() => typeof(T).GetFields(
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.DeclaredOnly)
            .Select(x => x.GetValue(null))
            .Cast<T>());
}
