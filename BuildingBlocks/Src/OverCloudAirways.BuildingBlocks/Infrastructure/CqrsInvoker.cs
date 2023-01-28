using Autofac;
using MediatR;
using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Application.Queries;

namespace OverCloudAirways.BuildingBlocks.Infrastructure;

public class CqrsInvoker
{
    public async Task<TResult> CommandAsync<TResult>(ICommand<TResult> command)
    {
        using var scope = CompositionRoot.BeginLifetimeScope();
        var mediator = scope.Resolve<IMediator>();

        return await mediator.Send(command);
    }

    public async Task CommandAsync(ICommand command)
    {
        using var scope = CompositionRoot.BeginLifetimeScope();
        var mediator = scope.Resolve<IMediator>();

        await mediator.Send(command);
    }

    public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
    {
        using var scope = CompositionRoot.BeginLifetimeScope();
        var mediator = scope.Resolve<IMediator>();

        return await mediator.Send(query);
    }
}
