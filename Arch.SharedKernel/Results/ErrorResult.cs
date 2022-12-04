namespace Arch.SharedKernel.Results;

public class ErrorResult
{
    private const string Separator = "||";

    public ErrorResult(
        string? code,
        string? message)
    {
        this.Code = code;
        this.Message = message;
        this.TimeGenerated = DateTime.UtcNow;
    }

    public string? Code { get; }

    public string? Message { get; }

    public DateTime TimeGenerated { get; }

    public string Serialize()
    {
        return $"{this.Code}{Separator}{this.Message}";
    }

    public static ErrorResult Deserialize(string? serialized)
    {
        switch (serialized)
        {
            case null:
                throw new ArgumentNullException(nameof(serialized));
            case "A non-empty request body is required.":
                return GeneralErrors.ValueIsRequired();
        }

        var data = serialized.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries);
        if (data.Length < 2)
        {
            throw new InvalidOperationException($"Invalid error serialization: '{serialized}'");
        }

        return new ErrorResult(data[0], data[1]);
    }
}
