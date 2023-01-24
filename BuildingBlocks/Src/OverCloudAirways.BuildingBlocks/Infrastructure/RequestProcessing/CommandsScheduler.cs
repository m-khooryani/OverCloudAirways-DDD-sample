using DArch.Application.Configuration.Commands;
using DArch.Application.Contracts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.RequestProcessing;

internal class CommandsScheduler : ICommandsScheduler
{
    private readonly ILogger _logger;
    private readonly IOutboxRepository _repository;

    public CommandsScheduler(
        ILogger logger,
        IOutboxRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task EnqueueAsync(ICommand command)
    {
        var queuedCommand = OutboxMessage.Create(
            Clock.Now,
            command.GetType().FullName!,
            JsonConvert.SerializeObject(command));

        var json = JsonConvert.SerializeObject(queuedCommand, Formatting.Indented);
        _logger.LogInformation("Enqueue Command:");
        _logger.LogInformation(json);

        await _repository.AddAsync(queuedCommand);
    }

    public async Task EnqueueAsync<TResult>(ICommand<TResult> command)
    {
        var queuedCommand = OutboxMessage.Create(
            Clock.Now,
            command.GetType().FullName!,
            JsonConvert.SerializeObject(command));

        var json = JsonConvert.SerializeObject(queuedCommand, Formatting.Indented);
        _logger.LogInformation("Enqueue Command:");
        _logger.LogInformation(json);

        await _repository.AddAsync(queuedCommand);
    }
}
