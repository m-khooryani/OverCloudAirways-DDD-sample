using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.BookingService.Domain.Flights.Events;

public record FlightScheduledDomainEvent(
    FlightId FlightId,
    string Number,
    AirportId DepartureAirportId,
    AirportId DestinationAirportId,
    DateTimeOffset DepartureTime,
    DateTimeOffset ArrivalTime,
    string Route,
    int Distance,
    AircraftId AircraftId,
    int AvailableSeats,
    int BookedSeats,
    double MaximumLuggageWeight) : DomainEvent(FlightId);
