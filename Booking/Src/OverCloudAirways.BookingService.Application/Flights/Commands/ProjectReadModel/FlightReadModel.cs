using OverCloudAirways.BuildingBlocks.Application.Models;

namespace OverCloudAirways.BookingService.Application.Flights.Commands.ProjectReadModel;

internal record FlightReadModel(
    Guid FlightId,
    string Number,
    string DepartureAirport,
    string DestinationAirport,
    DateTimeOffset DepartureTime,
    DateTimeOffset ArrivalTime,
    string Route,
    int Distance,
    string AircraftModel,
    int AvailableSeats,
    int BookedSeats,
    double MaximumLuggageWeight,
    decimal EconomyPrice,
    decimal FirstClassPrice) : ReadModel(FlightId.ToString(), FlightId.ToString());
