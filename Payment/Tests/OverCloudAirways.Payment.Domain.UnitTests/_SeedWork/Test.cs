using System.Diagnostics.CodeAnalysis;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;
using OverCloudAirways.BuildingBlocks.Domain.Exceptions;
using OverCloudAirways.BuildingBlocks.Domain.Models;
using Xunit;

namespace OverCloudAirways.PaymentService.Domain.UnitTests._SeedWork;

[ExcludeFromCodeCoverage]
public abstract class Test
{
    public static DomainEventAssertion<T> AssertPublishedDomainEvent<T>(IAggregateRoot aggregate)
        where T : DomainEvent
    {
        var domainEvent = aggregate.DomainEvents
            .OfType<T>()
            .FirstOrDefault();

        if (domainEvent is null)
        {
            throw new Exception($"{typeof(T).Name} is not published.");
        }

        return new DomainEventAssertion<T>(domainEvent);
    }

    public static DomainEventAssertion<T> AssertPublishedDomainEvent<T>(IAggregateRoot aggregate, int count)
        where T : DomainEvent
    {
        var domainEvents = aggregate.DomainEvents
            .OfType<T>()
            .ToList();

        if (domainEvents.Count != count)
        {
            throw new Exception($"{typeof(T).Name} was not published {count} times.");
        }

        var domainEvent = domainEvents.Last();

        return new DomainEventAssertion<T>(domainEvent);
    }

    public static async Task AssertViolatedRuleAsync<TRule>(Func<Task> testDelegate)
        where TRule : class, IBusinessRule
    {
        var businessRuleValidationException = await Assert.ThrowsAsync<BusinessRuleValidationException>(testDelegate);
        if (businessRuleValidationException != null)
        {
            Assert.IsType<TRule>(businessRuleValidationException.BrokenRule);
        }
    }
}
