using System.Collections.Concurrent;
using Azure.Messaging.ServiceBus;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.AzureServiceBus;

internal class ServiceBusSenderFactory : IServiceBusSenderFactory
{
    private readonly ServiceBusConfig _serviceBusConfig;
    private ServiceBusClient? _client = null;
    private static readonly object _lockObj = new();
    private readonly ConcurrentDictionary<string, ServiceBusSender> _senders = new();

    public ServiceBusSenderFactory(ServiceBusConfig serviceBusConfig)
    {
        _serviceBusConfig = serviceBusConfig;
    }

    public ServiceBusSender CreateSender(string queueOrTopicName)
    {
        lock (_lockObj)
        {
            if (_client is null || _client.IsClosed)
            {
                _senders.Clear();
                _client = new ServiceBusClient(_serviceBusConfig.ConnectionString);
            }
            return _senders.GetOrAdd(queueOrTopicName, _client.CreateSender);
        }
    }
}
