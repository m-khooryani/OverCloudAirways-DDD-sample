namespace OverCloudAirways.BuildingBlocks.Domain.Models;

public class AggregateRootHistoryItem
{
    public string Id { get; private set; }
    public string AggregateId { get; private set; }
    public int Version { get; private set; }
    public Guid UserId { get; private set; }
    public string Username { get; private set; }
    public string EventType { get; private set; }
    public DateTimeOffset Datetime { get; private set; }
    public string AggregateType { get; private set; }
    public string Data { get; private set; }

    private AggregateRootHistoryItem()
    {
    }

    private AggregateRootHistoryItem(
        string id,
        string aggregateId,
        int version,
        Guid userId,
        string username,
        string eventType,
        DateTimeOffset dateTime,
        string type,
        string data)
        : this()
    {
        Id = id;
        AggregateId = aggregateId;
        Version = version;
        UserId = userId;
        Username = username;
        EventType = eventType;
        Datetime = dateTime;
        AggregateType = type;
        Data = data;
    }

    public static AggregateRootHistoryItem Create(
        string aggregateId,
        int version,
        Guid userId,
        string username,
        string eventType,
        DateTimeOffset dateTime,
        string type,
        string data)
    {
        return new AggregateRootHistoryItem(
            Guid.NewGuid().ToString(),
            aggregateId,
            version,
            userId,
            username,
            eventType,
            dateTime,
            type,
            data);
    }
}
