using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BookingService.IntegrationEvents.Flights;

public record FlightCanceledIntegrationEvent(FlightId FlightId)
    : IntegrationEvent(FlightId, "booking-flight-canceled");
