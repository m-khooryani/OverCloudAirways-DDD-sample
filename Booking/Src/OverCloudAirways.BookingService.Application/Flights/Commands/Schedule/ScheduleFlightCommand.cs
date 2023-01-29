using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Application.Commands;

namespace OverCloudAirways.BookingService.Application.Flights.Commands.Schedule;

public record ScheduleFlightCommand(
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
    double MaximumLuggageWeight) : Command;
