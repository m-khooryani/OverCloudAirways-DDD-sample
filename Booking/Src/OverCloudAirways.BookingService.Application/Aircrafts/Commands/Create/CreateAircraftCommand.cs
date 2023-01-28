using FluentValidation;
using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BuildingBlocks.Application.Commands;

namespace OverCloudAirways.BookingService.Application.Aircrafts.Commands.Create;

public record CreateAircraftCommand(
    AircraftId AircraftId,
    string Type,
    string Manufacturer,
    string Model,
    int SeatingCapacity,
    int EconomyCostPerKM,
    int FirstClassCostPerKM,
    int Range,
    int CruisingAltitude,
    int MaxTakeoffWeight,
    int Length,
    int Wingspan,
    int Height,
    IReadOnlyList<Engine> Engines) : Command;
