using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BookingService.TestHelpers.Aircrafts;
using OverCloudAirways.BookingService.TestHelpers.Airports;
using OverCloudAirways.BookingService.TestHelpers.Flights;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;
using OverCloudAirways.BuildingBlocks.Domain.Exceptions;
using OverCloudAirways.BuildingBlocks.Domain.Models;
using Xunit;

namespace OverCloudAirways.BookingService.Domain.UnitTests._SeedWork;

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

    public static async Task AssertViolatedRuleAsync<TRule>(Func<Task> testDelegate)
        where TRule : class, IBusinessRule
    {
        var businessRuleValidationException = await Assert.ThrowsAsync<BusinessRuleValidationException>(testDelegate);
        if (businessRuleValidationException != null)
        {
            Assert.IsType<TRule>(businessRuleValidationException.BrokenRule);
        }
    }

    protected static async Task<Flight> GetFlight()
    {
        var aggregateRepository = Substitute.For<IAggregateRepository>();
        aggregateRepository
            .LoadAsync<Aircraft, AircraftId>(Arg.Any<AircraftId>())
            .Returns(new AircraftBuilder().Build());

        var codeChecker = Substitute.For<IAirportCodeUniqueChecker>();
        codeChecker.IsUniqueAsync(Arg.Any<string>()).Returns(true);

        var airport = await new AirportBuilder()
            .SetAirportCodeUniqueChecker(codeChecker)
            .BuildAsync();
        aggregateRepository
            .LoadAsync<Airport, AirportId>(Arg.Any<AirportId>())
            .Returns(airport);

        var builder = new FlightBuilder()
            .SetAggregateRepository(aggregateRepository);

        var flight = await builder.BuildAsync();
        return flight;
    }
}
