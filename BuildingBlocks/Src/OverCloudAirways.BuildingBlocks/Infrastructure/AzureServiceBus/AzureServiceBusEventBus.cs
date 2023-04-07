using System.Text;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.AzureServiceBus;

internal class AzureServiceBusEventBus : IEventBus
{
    private readonly IServiceBusSenderFactory _senderFactory;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly ILogger _logger;

    public AzureServiceBusEventBus(
        IServiceBusSenderFactory senderFactory,
        IJsonSerializer jsonSerializer,
        ILogger logger)
    {
        _senderFactory = senderFactory;
        _jsonSerializer = jsonSerializer;
        _logger = logger;
    }

    private void LogMessage(string queueOrTopicName, string? sessionId, string data)
    {
        _logger.LogInformation("Publishing message to queue");
        _logger.LogInformation($"QueueOrTopicName: {queueOrTopicName}");
        _logger.LogInformation($"SessionId: {sessionId}");
        _logger.LogInformation($"Data: {data}");
    }

    private static ServiceBusMessage CreateMessage(string data, string? sessionId = null)
    {
        var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(data));

        if (sessionId != null)
        {
            message.SessionId = sessionId;
        }

        return message;
    }

    private async Task<string> SendMessageAsync(string queueOrTopicName, ServiceBusMessage message, DateTimeOffset? scheduleEnqueueTime = null)
    {
        var sender = _senderFactory.CreateSender(queueOrTopicName);

        if (scheduleEnqueueTime.HasValue)
        {
            var sequenceNumber = await sender.ScheduleMessageAsync(message, scheduleEnqueueTime.Value);
            return sequenceNumber.ToString();
        }
        else
        {
            await sender.SendMessageAsync(message);
            return string.Empty;
        }
    }

    public async Task PublishAsync(IntegrationEvent @event)
    {
        var eventType = @event.GetType();
        _logger.LogInformation($"Publishing {eventType.FullName}...");

        var json = _jsonSerializer.Serialize(@event);
        var message = CreateMessage(json, @event.AggregateId.ToString());
        LogMessage(@event.IntegrationEventName, @event.AggregateId.ToString(), json);
        await SendMessageAsync(@event.IntegrationEventName, message);
    }

    async Task IEventBus.PublishAsync(string queue, OutboxMessage outboxMessage)
    {
        var json = _jsonSerializer.Serialize(outboxMessage);
        var message = CreateMessage(json, outboxMessage.SessionId);
        LogMessage(queue, outboxMessage.SessionId, json);
        await SendMessageAsync(queue, message);
    }

    async Task<string> IEventBus.ScheduleAsync(string queue, OutboxMessage outboxMessage, DateTimeOffset dateTimeOffset)
    {
        var json = _jsonSerializer.Serialize(outboxMessage);
        var message = CreateMessage(json, outboxMessage.SessionId);
        LogMessage(queue, outboxMessage.SessionId, json);
        return await SendMessageAsync(queue, message, dateTimeOffset);
    }
}
