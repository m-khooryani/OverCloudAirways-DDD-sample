using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.BookingService.Domain.Flights.Events;

public record FlightPriceChargedDomainEvent(
    FlightId FlightId,
    decimal EconomyPrice,
    decimal FirstClassPrice) : DomainEvent(FlightId);
