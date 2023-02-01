using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BookingService.IntegrationEvents.Flights;

public record FlightSeatsReservedIntegrationEvent(
    FlightId FlightId,
    string? TcpConnectionId,
    int SeatsCount) : IntegrationEvent(FlightId, "booking-flight-seats-reserved");
