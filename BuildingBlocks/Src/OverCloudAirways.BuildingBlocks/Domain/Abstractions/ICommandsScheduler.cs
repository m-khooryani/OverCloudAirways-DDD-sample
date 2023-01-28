using OverCloudAirways.BuildingBlocks.Application.Commands;

namespace OverCloudAirways.BuildingBlocks.Domain.Abstractions;

public interface ICommandsScheduler
{
    Task EnqueueAsync(ICommand command);
    Task EnqueueAsync<TResult>(ICommand<TResult> command);
}
