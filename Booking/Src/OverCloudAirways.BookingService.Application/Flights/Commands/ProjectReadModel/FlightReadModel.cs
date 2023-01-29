using OverCloudAirways.BuildingBlocks.Application.Models;

namespace OverCloudAirways.BookingService.Application.Flights.Commands.ProjectReadModel;

internal record FlightReadModel(
    Guid Id,
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
    decimal FirstClassPrice) : ReadModel(Id.ToString(), Id.ToString());
