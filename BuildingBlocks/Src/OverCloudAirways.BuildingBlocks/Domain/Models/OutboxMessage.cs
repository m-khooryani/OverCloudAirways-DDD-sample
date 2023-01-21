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

    public OutboxMessage(DateTimeOffset occurredOn, string type, string data, string? sessionId = null)
        : this()
    {
        Id = Guid.NewGuid();
        OccurredOn = occurredOn;
        Type = type;
        Data = data;
        SessionId = sessionId;
    }
}
