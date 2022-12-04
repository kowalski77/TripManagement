namespace Arch.SharedKernel.Results;

public static class GeneralErrors
{
    public static ErrorResult NotFound(Guid id, string argument)
    {
        var forId = id == Guid.Empty ? "" : $" for Id '{id}'";
        return new ErrorResult(ErrorConstants.RecordNotFound, $"Record {argument} not found {forId}");
    }

    public static ErrorResult ValueIsRequired() => new(ErrorConstants.ValueIsRequired, "Value is required");

    public static ErrorResult NotValidEmailAddress() => new(ErrorConstants.NotValidEmail, "Value is not a valid email.");

    public static ErrorResult InternalServerError(string message) => new(ErrorConstants.InternalServerError, message);

    public static ErrorResult IdNotValid(string message) => new(ErrorConstants.IdIsNullOrEmpty, message);
}
