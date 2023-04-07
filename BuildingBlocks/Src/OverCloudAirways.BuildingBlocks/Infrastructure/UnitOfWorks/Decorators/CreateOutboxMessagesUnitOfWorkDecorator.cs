using Autofac;
using Autofac.Core;
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
    private readonly IUserAccessor _userAccessor;
    private readonly IJsonSerializer _jsonSerializer;

    public CreateOutboxMessagesUnitOfWorkDecorator(
        IUnitOfWork decorated,
        IAggregateRepository aggregateRepository,
        ILifetimeScope scope,
        IOutboxRepository repository,
        IUserAccessor userAccessor,
        IJsonSerializer jsonSerializer)
    {
        _decorated = decorated;
        _aggregateRepository = aggregateRepository;
        _scope = scope;
        _repository = repository;
        _userAccessor = userAccessor;
        _jsonSerializer = jsonSerializer;
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

            var outboxMessage = OutboxMessage.Create(
                _jsonSerializer,
                Clock.Now, 
                domainPolicy, 
                _userAccessor.UserId,
                _userAccessor.TcpConnectionId,
                aggregate.Id.ToString());
            await _repository.AddAsync(outboxMessage);
        }
    }

    private DomainEventPolicy? GetDomainEventPolicy(DomainEvent domainEvent)
    {
        var domainEvenPolicyType = typeof(DomainEventPolicy<>);
        var domainPolicyWithGenericType = domainEvenPolicyType.MakeGenericType(domainEvent.GetType());
        var domainPolicy = _scope.ResolveOptional(domainPolicyWithGenericType, new List<Parameter>
        {
            new NamedParameter("domainEvent", domainEvent)
        });

        return domainPolicy as DomainEventPolicy;
    }
}
