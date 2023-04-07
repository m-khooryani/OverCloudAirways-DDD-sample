using MediatR;
using Microsoft.Extensions.Logging;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.RequestProcessing.PolicyPipelines;

internal class PolicyLoggingDecorator<T> : INotificationHandler<T>
       where T : DomainEventPolicy
{
    private readonly ILogger _logger;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly INotificationHandler<T> _decorated;

    public PolicyLoggingDecorator(
        ILogger logger,
        IJsonSerializer jsonSerializer,
        INotificationHandler<T> decorated)
    {
        _logger = logger;
        _jsonSerializer = jsonSerializer;
        _decorated = decorated;
    }

    public async Task Handle(T notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{notification.GetType().Name} is processing: {Environment.NewLine}{_jsonSerializer.SerializeIndented(notification)}");
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