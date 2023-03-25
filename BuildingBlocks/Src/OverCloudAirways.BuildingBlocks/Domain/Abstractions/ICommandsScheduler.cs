using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BuildingBlocks.Domain.Abstractions;

public interface ICommandsScheduler
{
    Task EnqueueAsync(ICommand command);
    Task EnqueueAsync<TResult>(ICommand<TResult> command);
    Task EnqueuePublishingEventAsync(IntegrationEvent integrationEvent);
    Task ScheduleAsync(ICommand command, DateTimeOffset date);
}
