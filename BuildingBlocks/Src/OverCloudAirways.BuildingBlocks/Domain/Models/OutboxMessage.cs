using Newtonsoft.Json;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;

namespace OverCloudAirways.BuildingBlocks.Domain.Models;

public class OutboxMessage
{
    public Guid Id { get; set; }
    public string? SessionId { get; set; }
    public DateTimeOffset OccurredOn { get; set; }
    public string AssemblyName { get; set; }
    public string Type { get; set; }
    public string Data { get; set; }
    public DateTimeOffset? ProcessedDate { get; set; }
    public DateTimeOffset? ProcessingDate { get; set; }
    public string? Error { get; set; }
    public Guid? UserId { get; internal set; }
    public string? TcpConnectionId { get; internal set; }

    public bool IsInstantProcessing => ProcessingDate is null;

    private OutboxMessage()
    {
    }

    public static OutboxMessage Create(
        DateTimeOffset occurredOn, 
        object obj, 
        Guid? userId,
        string? tcpConnectionId,
        string? sessionId = null)
    {
        return Create(occurredOn, obj, userId, tcpConnectionId, null, sessionId);
    }

    public static OutboxMessage CreateDelayed(
        DateTimeOffset occurredOn, 
        object obj, 
        Guid? userId,
        string? tcpConnectionId,
        DateTimeOffset processingDate,
        string? sessionId = null)
    {
        return Create(occurredOn, obj, userId, tcpConnectionId, processingDate, sessionId);
    }

    private static OutboxMessage Create(
        DateTimeOffset occurredOn,
        object obj,
        Guid? userId,
        string? tcpConnectionId,
        DateTimeOffset? processingDate,
        string? sessionId = null)
    {
        var message = new OutboxMessage();
        message.Id = Guid.NewGuid();
        message.OccurredOn = occurredOn;
        message.AssemblyName = obj.GetType()!.Assembly!.GetName()!.Name!;
        message.Type = obj.GetType().FullName!;

        var settings = new JsonSerializerSettings();
        settings.Converters.Add(new EnumerationJsonConverter());
        message.Data = JsonConvert.SerializeObject(obj, settings);
        message.SessionId = sessionId;
        message.UserId = userId;
        message.TcpConnectionId = tcpConnectionId;
        message.ProcessingDate = processingDate;

        return message;
    }
}
