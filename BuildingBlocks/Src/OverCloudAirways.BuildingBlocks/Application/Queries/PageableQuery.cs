namespace OverCloudAirways.BuildingBlocks.Application.Queries;

public abstract record PageableQuery<TResult> : Query<PagedDto<TResult>>
{
    public int PageNumber { get; }
    public int PageSize { get; }

    public PageableQuery(int pageNumber, int pageSize)
        : base()
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}
