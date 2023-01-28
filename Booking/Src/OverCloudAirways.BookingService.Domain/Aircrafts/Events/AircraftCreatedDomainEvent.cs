using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.BookingService.Domain.Aircrafts.Events;

public record AircraftCreatedDomainEvent(
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
    IReadOnlyList<Engine> Engines) : DomainEvent(AircraftId);