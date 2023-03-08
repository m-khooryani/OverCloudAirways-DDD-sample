using System.Collections.Concurrent;
using Autofac;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OverCloudAirways.BuildingBlocks.Application.Commands.PublishIntegrationEvent;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.BuildingBlocks.Infrastructure;
using OverCloudAirways.BuildingBlocks.Infrastructure.Layers;
using OverCloudAirways.BuildingBlocks.Infrastructure.RetryPolicy;
using Polly;

namespace OverCloudAirways.BuildingBlocks.Application.Commands.ProcessOutboxMessage;

internal class ProcessOutboxCommandHandler : CommandHandler<ProcessOutboxCommand>
{
    private readonly IOutboxRepository _outboxRepository;
    private readonly AssemblyLayers _layers;
    private readonly PollyConfig _pollyConfig;
    private readonly ILogger _logger;
    private int _executedTimes;
    private readonly ConcurrentDictionary<string, Type?> _typeCache;

    public ProcessOutboxCommandHandler(
        IOutboxRepository outboxRepository,
        AssemblyLayers layers,
        PollyConfig pollyConfig,
        ILogger logger)
    {
        _outboxRepository = outboxRepository;
        _layers = layers;
        _pollyConfig = pollyConfig;
        _logger = logger;
        _executedTimes = 0;
        _typeCache = new();
    }

    public override async Task<Unit> HandleAsync(ProcessOutboxCommand request, CancellationToken cancellationToken)
    {
        var messageId = Guid.Parse(request.MessageId);
        var outboxMessage = await _outboxRepository
            .LoadAsync(messageId, cancellationToken);

        if (outboxMessage is null)
        {
            _logger.LogWarning("outbox message not found! skipping.");
            return Unit.Value;
        }
        var policy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(_pollyConfig.SleepDurations);
        var result = await policy.ExecuteAndCaptureAsync(() => ProcessCommandAndDeleteAsync(outboxMessage, cancellationToken));
        if (result.Outcome == OutcomeType.Failure)
        {
            _logger.LogError("failed to process outbox message.");
            outboxMessage.Error = result.FinalException.ToString();
            outboxMessage.ProcessedDate = Clock.Now;
        }

        return Unit.Value;
    }

    private async Task ProcessCommandAndDeleteAsync(OutboxMessage outboxMessage, CancellationToken cancellationToken)
    {
        _executedTimes++;

        Type? type = _typeCache.GetOrAdd(outboxMessage.Type, typeAsStr =>
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.GetName().Name == outboxMessage.AssemblyName);
            return assembly?.GetType(typeAsStr);
        });
        if (type is null)
        {
            throw new InvalidOperationException($"Could not find type '{outboxMessage.Type}'");
        }

        var settings = new JsonSerializerSettings();
        settings.Converters.Add(new EnumerationJsonConverter());
        var deserializedMessage = JsonConvert.DeserializeObject(outboxMessage.Data, type, settings) as dynamic;

        using var scope = CompositionRoot.BeginLifetimeScope();
        var mediator = scope.Resolve<IMediator>();
        if (type.IsAssignableTo(typeof(INotification)))
        {
            await mediator.Publish((deserializedMessage as DomainEventPolicy)!, cancellationToken);
            var unitOfWork = scope.Resolve<IUnitOfWork>();
            await unitOfWork.CommitAsync(cancellationToken);
        }
        else if (type.IsAssignableTo(typeof(ICommand)) ||
            type.IsAssignableTo(typeof(ICommand<>)))
        {
            await mediator.Send(deserializedMessage, cancellationToken);
        }
        else
        {
            var publishIntegrationEventCommand = new PublishIntegrationEventCommand(deserializedMessage as IntegrationEvent);
            await mediator.Send(publishIntegrationEventCommand, cancellationToken);
        }

        _outboxRepository.Remove(outboxMessage);
    }
}
