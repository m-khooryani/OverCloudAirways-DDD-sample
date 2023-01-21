using MediatR;

namespace DArch.Application.Contracts;

public interface IQuery<out TResult> : IRequest<TResult>, IQueryBase
{
}

public interface IQueryBase { }
