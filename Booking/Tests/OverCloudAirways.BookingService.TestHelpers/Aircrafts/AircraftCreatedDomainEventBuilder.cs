using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BookingService.Domain.Aircrafts.Events;

namespace OverCloudAirways.BookingService.TestHelpers.Aircrafts;

public class AircraftCreatedDomainEventBuilder
{
    private AircraftId _aircraftId = AircraftId.New();
    private string _type = "Boeing 747";
    private string _manufacturer = "Boeing";
    private string _model = "747-400";
    private int _seatingCapacity = 366;
    private int _economyCostPerKM = 100;
    private int _firstClassCostPerKM = 100;
    private int _range = 7500;
    private int _cruisingAltitude = 35000;
    private int _maxTakeoffWeight = 875000;
    private int _length = 231;
    private int _wingspan = 211;
    private int _height = 63;
    private List<Engine> _engines = new()
    {
        new EngineBuilder().Build(),
        new EngineBuilder().Build()
    };

    public AircraftCreatedDomainEvent Build()
    {
        return new AircraftCreatedDomainEvent(
            _aircraftId,
            _type,
            _manufacturer,
            _model,
            _seatingCapacity,
            _economyCostPerKM,
            _firstClassCostPerKM,
            _range,
            _cruisingAltitude,
            _maxTakeoffWeight,
            _length,
            _wingspan,
            _height,
            _engines);
    }

    public AircraftCreatedDomainEventBuilder SetAircraftId(AircraftId aircraftId)
    {
        _aircraftId = aircraftId;
        return this;
    }

    public AircraftCreatedDomainEventBuilder SetType(string type)
    {
        _type = type;
        return this;
    }

    public AircraftCreatedDomainEventBuilder SetManufacturer(string manufacturer)
    {
        _manufacturer = manufacturer;
        return this;
    }

    public AircraftCreatedDomainEventBuilder SetModel(string model)
    {
        _model = model;
        return this;
    }

    public AircraftCreatedDomainEventBuilder SetSeatingCapacity(int seatingCapacity)
    {
        _seatingCapacity = seatingCapacity;
        return this;
    }

    public AircraftCreatedDomainEventBuilder SetEconomyCostPerKM(int economyCostPerKM)
    {
        _economyCostPerKM = economyCostPerKM;
        return this;
    }

    public AircraftCreatedDomainEventBuilder SetFirstClassCostPerKM(int firstClassCostPerKM)
    {
        _firstClassCostPerKM = firstClassCostPerKM;
        return this;
    }

    public AircraftCreatedDomainEventBuilder SetRange(int range)
    {
        _range = range;
        return this;
    }

    public AircraftCreatedDomainEventBuilder SetCruisingAltitude(int cruisingAltitude)
    {
        _cruisingAltitude = cruisingAltitude;
        return this;
    }

    public AircraftCreatedDomainEventBuilder SetMaxTakeoffWeight(int maxTakeoffWeight)
    {
        _maxTakeoffWeight = maxTakeoffWeight;
        return this;
    }

    public AircraftCreatedDomainEventBuilder SetLength(int length)
    {
        _length = length;
        return this;
    }

    public AircraftCreatedDomainEventBuilder SetWingspan(int wingspan)
    {
        _wingspan = wingspan;
        return this;
    }

    public AircraftCreatedDomainEventBuilder SetHeight(int height)
    {
        _height = height;
        return this;
    }

    public AircraftCreatedDomainEventBuilder AddToEngine(Engine engine)
    {
        _engines.Add(engine);
        return this;
    }
}
