using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

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
        IJsonSerializer jsonSerializer,
        DateTimeOffset occurredOn,
        object obj,
        Guid? userId,
        string? tcpConnectionId,
        string? sessionId = null)
    {
        return Create(jsonSerializer, occurredOn, obj, userId, tcpConnectionId, null, sessionId);
    }

    public static OutboxMessage CreateDelayed(
        IJsonSerializer jsonSerializer,
        DateTimeOffset occurredOn,
        object obj,
        Guid? userId,
        string? tcpConnectionId,
        DateTimeOffset processingDate,
        string? sessionId = null)
    {
        return Create(jsonSerializer, occurredOn, obj, userId, tcpConnectionId, processingDate, sessionId);
    }

    private static OutboxMessage Create(
        IJsonSerializer jsonSerializer,
        DateTimeOffset occurredOn,
        object obj,
        Guid? userId,
        string? tcpConnectionId,
        DateTimeOffset? processingDate,
        string? sessionId = null)
    {
        sessionId ??= Guid.NewGuid().ToString();
        var message = new OutboxMessage();
        message.Id = Guid.NewGuid();
        message.OccurredOn = occurredOn;
        message.AssemblyName = obj.GetType()!.Assembly!.GetName()!.Name!;
        message.Type = obj.GetType().FullName!;

        message.Data = jsonSerializer.Serialize(obj);
        message.SessionId = sessionId;
        message.UserId = userId;
        message.TcpConnectionId = tcpConnectionId;
        message.ProcessingDate = processingDate;

        return message;
    }
}
