namespace OverCloudAirways.BuildingBlocks.Application.Queries;

public abstract record Query<TResult> : Query, IQuery<TResult>
{
    public Guid InternalProcessId { get; }

    protected Query()
    {
        InternalProcessId = Guid.NewGuid();
    }
}

public abstract record Query
{
    internal Query()
    {

    }
}
