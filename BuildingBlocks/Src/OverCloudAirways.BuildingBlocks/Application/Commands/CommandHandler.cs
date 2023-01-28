using MediatR;

namespace OverCloudAirways.BuildingBlocks.Application.Commands;

public abstract class CommandHandler<TCommand> : IRequestHandler<TCommand> where TCommand : ICommand
{
    public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
    {
        await HandleAsync(request, cancellationToken);
        return Unit.Value;
    }

    public abstract Task HandleAsync(TCommand command, CancellationToken cancellationToken);
}

public abstract class CommandHandler<TCommand, TResult> :
    IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{
    public async Task<TResult> Handle(TCommand request, CancellationToken cancellationToken)
    {
        return await HandleAsync(request, cancellationToken);
    }

    public abstract Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken);
}
