using MediatR;

namespace DArch.Application.Contracts;

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

