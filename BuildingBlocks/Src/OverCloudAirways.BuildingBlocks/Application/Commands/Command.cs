namespace OverCloudAirways.BuildingBlocks.Application.Commands;

public abstract record Command : ICommand
{
    public Guid InternalProcessId { get; }

    protected Command()
    {
        InternalProcessId = Guid.NewGuid();
    }
}

public abstract record Command<TResult> : ICommand<TResult>
{
    public Guid InternalProcessId { get; }

    protected Command()
    {
        InternalProcessId = Guid.NewGuid();
    }
}
