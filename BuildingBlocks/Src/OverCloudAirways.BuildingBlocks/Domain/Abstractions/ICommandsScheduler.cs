using DArch.Application.Contracts;

namespace DArch.Application.Configuration.Commands;

public interface ICommandsScheduler
{
    Task EnqueueAsync(ICommand command);
    Task EnqueueAsync<TResult>(ICommand<TResult> command);
}
