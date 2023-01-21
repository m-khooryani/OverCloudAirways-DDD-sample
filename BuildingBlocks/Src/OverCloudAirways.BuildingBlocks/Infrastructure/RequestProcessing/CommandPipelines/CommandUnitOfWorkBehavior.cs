using DArch.Application.Contracts;
using MediatR;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace DArch.Infrastructure.Processing;

internal class CommandUnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public CommandUnitOfWorkBehavior(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var result = await next();
        await _unitOfWork.CommitAsync(cancellationToken);

        return result;
    }
}
