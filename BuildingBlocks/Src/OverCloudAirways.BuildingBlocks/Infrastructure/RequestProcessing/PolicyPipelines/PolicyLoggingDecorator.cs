using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.RequestProcessing.PolicyPipelines;

internal class PolicyLoggingDecorator<T> : INotificationHandler<T>
       where T : DomainEventPolicy
{
    private readonly ILogger _logger;
    private readonly INotificationHandler<T> _decorated;

    public PolicyLoggingDecorator(
        ILogger logger,
        INotificationHandler<T> decorated)
    {
        _logger = logger;
        _decorated = decorated;
    }

    public async Task Handle(T notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{notification.GetType().Name} is processing: {Environment.NewLine}{JsonConvert.SerializeObject(notification, Formatting.Indented)}");
        try
        {
            await _decorated.Handle(notification, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unhandled Exception:{Environment.NewLine}{ex}");
            throw;
        }
        finally
        {
            _logger.LogDebug($"{notification.GetType().Name} is processed.");
        }
    }
}