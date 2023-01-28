using MediatR;

namespace OverCloudAirways.BuildingBlocks.Application.Commands;

public interface ICommand<out TResult> : IRequest<TResult>, ICommandBase
{
    /// <summary>
    /// for tracking purposes
    /// </summary>
    Guid InternalProcessId { get; }
}

public interface ICommand : ICommand<Unit>
{
}

public interface ICommandBase { }

