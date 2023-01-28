using OverCloudAirways.BookingService.Domain.Aircrafts.Events;
using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BookingService.Domain.Aircrafts;

public class Aircraft : AggregateRoot<AircraftId>
{
    public string Type { get; private set; }
    public string Manufacturer { get; private set; }
    public string Model { get;  private set; }
    public int SeatingCapacity { get; private set; }
    public int EconomyCostPerKM { get; private set; }
    public int FirstClassCostPerKM { get; private set; }
    public int Range { get; private set; }
    public int CruisingAltitude { get; private set; }
    public int MaxTakeoffWeight { get; private set; }
    public int Length { get; private set; }
    public int Wingspan { get; private set; }
    public int Height { get; private set; }
    private List<Engine> _engines;

    public IReadOnlyCollection<Engine> Engines => _engines.AsReadOnly();

    private Aircraft()
    {
    }

    public static Aircraft Create(
        AircraftId id, 
        string type, 
        string manufacturer, 
        string model, 
        int seatingCapacity, 
        int economyCostPerKM, 
        int firstClassCostPerKM, 
        int range, 
        int cruisingAltitude, 
        int maxTakeoffWeight, 
        int length, 
        int wingspan, 
        int height, 
        IReadOnlyList<Engine> engines)
    {
        var @event = new AircraftCreatedDomainEvent(
            id,
            type,
            manufacturer,
            model,
            seatingCapacity,
            economyCostPerKM,
            firstClassCostPerKM,
            range,
            cruisingAltitude,
            maxTakeoffWeight,
            length,
            wingspan,
            height,
            engines);

        var aircraft = new Aircraft();
        aircraft.Apply(@event);

        return aircraft;
    }

    protected void When(AircraftCreatedDomainEvent @event)
    {
        Id = @event.AircraftId;
        Type = @event.Type;
        Manufacturer = @event.Manufacturer;
        Model = @event.Model;
        SeatingCapacity = @event.SeatingCapacity;
        EconomyCostPerKM = @event.EconomyCostPerKM;
        FirstClassCostPerKM = @event.FirstClassCostPerKM;
        Range = @event.Range;
        CruisingAltitude = @event.CruisingAltitude;
        MaxTakeoffWeight = @event.MaxTakeoffWeight;
        Length = @event.Length;
        Wingspan = @event.Wingspan;
        Height = @event.Height;
        _engines = @event.Engines.ToList();
    }
}
