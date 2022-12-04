using Arch.SharedKernel;
using Arch.SharedKernel.DomainDriven;
using Arch.SharedKernel.Results;

namespace TripManagement.Domain.TripsAggregate;

public class UserId : ValueObject
{
    private UserId(Guid value) => Value = value.NonNull();

    public Guid Value { get; }

    public static Result<UserId> CreateInstance(Guid value) => value == Guid.Empty ?
            GeneralErrors.IdNotValid($"{nameof(UserId)} is not valid") :
            new UserId(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
