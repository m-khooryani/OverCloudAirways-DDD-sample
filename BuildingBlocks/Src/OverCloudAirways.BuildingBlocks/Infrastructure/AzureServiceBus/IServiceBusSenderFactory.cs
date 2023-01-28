using Azure.Messaging.ServiceBus;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.AzureServiceBus;

public interface IServiceBusSenderFactory
{
    ServiceBusSender CreateSender(string queueOrTopicName);
}
