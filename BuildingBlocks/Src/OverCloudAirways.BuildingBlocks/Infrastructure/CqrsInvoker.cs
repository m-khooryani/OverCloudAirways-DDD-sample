using Autofac;
using DArch.Application.Contracts;
using DArch.Infrastructure.Configuration;
using MediatR;

namespace DArch.Infrastructure;

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
