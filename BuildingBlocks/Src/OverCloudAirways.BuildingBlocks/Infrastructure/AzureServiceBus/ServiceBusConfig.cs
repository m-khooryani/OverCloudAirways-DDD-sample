namespace OverCloudAirways.BuildingBlocks.Infrastructure.AzureServiceBus;

public class ServiceBusConfig
{
    public string OutboxQueueName { get; init; } = string.Empty;
    public string ConnectionString { get; init; } = string.Empty;
}
