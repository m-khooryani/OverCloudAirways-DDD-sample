using Azure.Messaging.ServiceBus;

namespace DArch.Infrastructure.EventBus;

public interface IServiceBusSenderFactory
{
    ServiceBusSender CreateSender(string queueOrTopicName);
}
