using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OverCloudAirways.BuildingBlocks.Application.Commands.ProcessOutboxMessage;
using OverCloudAirways.BuildingBlocks.Infrastructure;

namespace OverCloudAirways.IdentityService.API;

public class OutboxTrigger
{
    private readonly ILogger _logger;
    private readonly CqrsInvoker _cqrsInvoker;

    public OutboxTrigger(
        ILoggerFactory loggerFactory,
        CqrsInvoker cqrsInvoker)
    {
        _logger = loggerFactory.CreateLogger<OutboxTrigger>();
        _cqrsInvoker = cqrsInvoker;
    }

    [Function("OutboxTriggerFunction")]
    public async Task Run([ServiceBusTrigger(
        queueName: "Identity-Outbox",
        Connection = "ServiceBusConnectionString",
        IsSessionsEnabled = true)] string myQueueItem)
    {
        _logger.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");

        var outboxMessage = JsonConvert.DeserializeObject<OutboxMessageReference>(myQueueItem);
        try
        {
            await _cqrsInvoker.CommandAsync(new ProcessOutboxCommand(outboxMessage.OutboxMessageId));
        }
        catch (Exception ex)
        {
            _logger.LogCritical("Processing OutboxMessage failed");
            _logger.LogCritical(ex.ToString());
            throw;
        }
    }

    private record OutboxMessageReference(Guid OutboxMessageId);
}
