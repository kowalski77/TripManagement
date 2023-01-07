using System.Text.Json;

namespace Arch.SharedKernel.Outbox;

public static class OutboxSerializer
{
    public static async Task<TEvent> DeserializeAsync<TEvent>(this OutboxMessage outboxMessage) =>
        await outboxMessage.ToMemoryStream().ToEvent<TEvent>().ConfigureAwait(false);

    public static MemoryStream ToMemoryStream(this OutboxMessage outboxMessage)
    {
        MemoryStream stream = new();
        using Utf8JsonWriter writer = new(stream);

        writer.WriteRawValue(outboxMessage.NonNull().Data);
        writer.Flush();
        stream.Position = 0;

        return stream;
    }

    public static async Task<TEvent> ToEvent<TEvent>(this MemoryStream memoryStream) =>
        (await JsonSerializer.DeserializeAsync<TEvent>(memoryStream).ConfigureAwait(false))!;
}
