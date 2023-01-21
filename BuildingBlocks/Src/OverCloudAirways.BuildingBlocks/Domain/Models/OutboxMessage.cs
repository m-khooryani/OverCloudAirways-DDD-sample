namespace OverCloudAirways.BuildingBlocks.Domain.Models;

public class OutboxMessage
{
    public Guid Id { get; set; }
    public DateTime OccurredOn { get; set; }
    public string Type { get; set; }
    public string Data { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public string? Error { get; set; }

    private OutboxMessage()
    {
    }

    public OutboxMessage(DateTime occurredOn, string type, string data)
        : this()
    {
        Id = Guid.NewGuid();
        OccurredOn = occurredOn;
        Type = type;
        Data = data;
    }
}
