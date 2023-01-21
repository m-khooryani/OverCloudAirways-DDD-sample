using System.Text;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace DArch.Infrastructure.EventBus;

internal class AzureServiceBusEventBus : IEventBus
{
    private readonly IServiceBusSenderFactory _senderFactory;
    private readonly ILogger _logger;

    public AzureServiceBusEventBus(
        IServiceBusSenderFactory senderFactory, 
        ILogger logger)
    {
        _senderFactory = senderFactory;
        _logger = logger;
    }

    private Task Publish(string queueOrTopicName, string sessionId, string data)
    {
        _logger.LogInformation("Publishing message to queue");
        _logger.LogInformation($"QueueOrTopicName: {queueOrTopicName}");
        _logger.LogInformation($"SessionId: {sessionId}");
        _logger.LogInformation($"Data: {data}");
        var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(data))
        {
            SessionId = sessionId
        };

        var sender = _senderFactory.CreateSender(queueOrTopicName);
        return sender.SendMessageAsync(message);
    }

    public async Task PublishAsync(IntegrationEvent @event) 
    {
        var eventType = @event.GetType();
        _logger.LogInformation($"Publishing {eventType.FullName}...");

        var json = JsonConvert.SerializeObject(@event, Formatting.Indented);
        await Publish(@event.IntegrationEventName, @event.AggregateId.ToString()!, json);
    }

    async Task IEventBus.PublishAsync(string queue, string sessionId, OutboxMessage outboxMessage)
    {
        await Publish(queue, sessionId, JsonConvert.SerializeObject(outboxMessage, Formatting.Indented));
    }
}
