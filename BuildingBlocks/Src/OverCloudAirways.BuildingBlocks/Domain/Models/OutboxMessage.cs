namespace OverCloudAirways.BuildingBlocks.Domain.Models;

public class OutboxMessage
{
    public Guid Id { get; set; }
    public string? SessionId { get; set; }
    public DateTimeOffset OccurredOn { get; set; }
    public string Type { get; set; }
    public string Data { get; set; }
    public DateTimeOffset? ProcessedDate { get; set; }
    public string? Error { get; set; }

    private OutboxMessage()
    {
    }

    public static OutboxMessage Create(DateTimeOffset occurredOn, string type, string data, string? sessionId = null)
    {
        var message = new OutboxMessage();
        message.Id = Guid.NewGuid();
        message.OccurredOn = occurredOn;
        message.Type = type;
        message.Data = data;
        message.SessionId = sessionId;

        return message;
    }
}
