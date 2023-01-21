using Autofac;
using Autofac.Core;
using Newtonsoft.Json;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;
using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.UnitOfWorks.Decorators;

internal class CreateOutboxMessagesUnitOfWorkDecorator : IUnitOfWork
{
    private readonly IUnitOfWork _decorated;
    private readonly IAggregateRepository _aggregateRepository;
    private readonly ILifetimeScope _scope;
    private readonly IOutboxRepository _repository;

    public CreateOutboxMessagesUnitOfWorkDecorator(
        IUnitOfWork decorated,
        IAggregateRepository aggregateRepository,
        ILifetimeScope scope,
        IOutboxRepository repository)
    {
        _decorated = decorated;
        _aggregateRepository = aggregateRepository;
        _scope = scope;
        _repository = repository;
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        await CreateOutboxMessagesAsync();
        return await _decorated.CommitAsync(cancellationToken);
    }

    private async Task CreateOutboxMessagesAsync()
    {
        var aggregate = _aggregateRepository.GetModifiedAggregate();
        if (aggregate is null)
        {
            return;
        }
        var domainEvents = aggregate.DomainEvents;
        foreach (var domainEvent in domainEvents)
        {
            var domainPolicy = GetDomainEventPolicy(domainEvent);
            if (domainPolicy is null)
            {
                continue;
            }

            var data = JsonConvert.SerializeObject(domainPolicy);
            var outboxMessage = new OutboxMessage(Clock.Now, domainPolicy.GetType().Name, data, aggregate.Id.ToString());
            await _repository.AddAsync(outboxMessage);
        }
    }

    private DomainEventPolicy<DomainEvent>? GetDomainEventPolicy(DomainEvent domainEvent)
    {
        var domainEvenPolicyType = typeof(DomainEventPolicy<>);
        var domainPolicyWithGenericType = domainEvenPolicyType.MakeGenericType(domainEvent.GetType());
        var domainPolicy = _scope.ResolveOptional(domainPolicyWithGenericType, new List<Parameter>
        {
            new NamedParameter("DomainEvent", domainEvent)
        });

        return domainPolicy as DomainEventPolicy<DomainEvent>;
    }
}
