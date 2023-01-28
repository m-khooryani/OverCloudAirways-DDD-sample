using MediatR;

namespace OverCloudAirways.BuildingBlocks.Application.Queries;

public interface IQuery<out TResult> : IRequest<TResult>, IQueryBase
{
}

public interface IQueryBase { }
