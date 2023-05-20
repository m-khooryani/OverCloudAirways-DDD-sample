using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace OverCloudAirways.IdentityService.API;

public class OutboxTrigger
{
    private readonly ILogger _logger;

    public OutboxTrigger(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<OutboxTrigger>();
    }

    [Function("OutboxTriggerFunction")]
    public void Run([ServiceBusTrigger(
        queueName: "Identity-Outbox", 
        Connection = "ServiceBusConnectionString", 
        IsSessionsEnabled = true)] string myQueueItem)
    {
        _logger.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
    }
}
