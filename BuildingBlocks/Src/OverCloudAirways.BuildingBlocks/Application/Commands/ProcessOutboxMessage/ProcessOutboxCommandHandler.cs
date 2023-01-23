using Autofac;
using DArch.Application.Configuration.Commands;
using DArch.Infrastructure.CleanArchitecture;
using DArch.Infrastructure.RetryPolicy;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using Polly;

namespace DArch.Infrastructure.Configuration.Processing.Outbox;

internal class ProcessOutboxCommandHandler : CommandHandler<ProcessOutboxCommand>
{
    private readonly IOutboxRepository _outboxRepository;
    private readonly AssemblyLayers _layers;
    private readonly PollyConfig _pollyConfig;
    private readonly ILogger _logger;
    private int _executedTimes;

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
        await ProcessCommandAndDeleteAsync(outboxMessage);
        //var result = await policy.ExecuteAndCaptureAsync(() => ProcessCommandAndDeleteAsync(outboxMessage));
        //if (result.Outcome == OutcomeType.Failure)
        //{
        //    _logger.LogError("failed to process outbox message.");
        //    outboxMessage.Error = result.FinalException.ToString();
        //    outboxMessage.ProcessedDate = Clock.Now;
        //}

        return Unit.Value;
    }

    private async Task ProcessCommandAndDeleteAsync(OutboxMessage outboxMessage)
    {
        _executedTimes++;

        var type = _layers.ApplicationLayer.GetType(outboxMessage.Type);
        var commandToProcess = JsonConvert.DeserializeObject(outboxMessage.Data, type) as dynamic;

        using var scope = CompositionRoot.BeginLifetimeScope();
        var mediator = scope.Resolve<IMediator>();
        if (!outboxMessage.Type.Contains("Policy"))
        {
            await mediator.Send(commandToProcess);
        }
        else
        {
            await mediator.Publish(commandToProcess as DomainEventPolicy);
        }

        _outboxRepository.Remove(outboxMessage);
    }
}
