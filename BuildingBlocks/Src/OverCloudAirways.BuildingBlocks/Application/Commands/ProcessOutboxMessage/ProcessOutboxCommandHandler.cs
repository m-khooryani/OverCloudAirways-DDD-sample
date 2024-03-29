﻿using System.Collections.Concurrent;
using Autofac;
using MediatR;
using Microsoft.Extensions.Logging;
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
    private readonly IJsonSerializer _jsonSerializer;
    private readonly ILogger _logger;
    private int _executedTimes;
    private readonly ConcurrentDictionary<string, Type?> _typeCache;

    public ProcessOutboxCommandHandler(
        IOutboxRepository outboxRepository,
        AssemblyLayers layers,
        PollyConfig pollyConfig,
        IJsonSerializer jsonSerializer,
        ILogger logger)
    {
        _outboxRepository = outboxRepository;
        _layers = layers;
        _pollyConfig = pollyConfig;
        _jsonSerializer = jsonSerializer;
        _logger = logger;
        _executedTimes = 0;
        _typeCache = new();
    }

    public override async Task<Unit> HandleAsync(ProcessOutboxCommand request, CancellationToken cancellationToken)
    {
        var outboxMessage = await _outboxRepository
            .LoadAsync(request.MessageId, cancellationToken);

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

        var deserializedMessage = _jsonSerializer.Deserialize(outboxMessage.Data, type) as dynamic;

        using var scope = CompositionRoot.BeginLifetimeScope();
        var mediator = scope.Resolve<IMediator>();
        var accessor = scope.Resolve<IUserAccessor>();

        accessor.FullName = outboxMessage.FullName;
        accessor.UserId = outboxMessage.UserId.Value;
        accessor.TcpConnectionId = outboxMessage.TcpConnectionId;

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
