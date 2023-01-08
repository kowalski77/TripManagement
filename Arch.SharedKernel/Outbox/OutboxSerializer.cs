using System.Text.Json;

namespace Arch.SharedKernel.Outbox;

// TODO: maybe
public static class OutboxSerializer
{
    public static async Task<object> DeserializeAsync(this OutboxMessage outboxMessage)
    {
        using MemoryStream stream = new();
        using Utf8JsonWriter writer = new(stream);

        writer.WriteRawValue(outboxMessage.NonNull().Data);
        await writer.FlushAsync().ConfigureAwait(false);
        stream.Position = 0;

        var result = (await JsonSerializer.DeserializeAsync(stream, outboxMessage.Type).ConfigureAwait(false))!;

        return result;
    }
}
