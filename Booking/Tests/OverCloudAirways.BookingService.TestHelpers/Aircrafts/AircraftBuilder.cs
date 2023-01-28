using OverCloudAirways.BookingService.Domain.Aircrafts;

namespace OverCloudAirways.BookingService.TestHelpers.Aircrafts;

public class AircraftBuilder
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

    public Aircraft Build()
    {
        return Aircraft.Create(
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

    public AircraftBuilder SetAircraftId(AircraftId aircraftId)
    {
        _aircraftId = aircraftId;
        return this;
    }

    public AircraftBuilder SetType(string type)
    {
        _type = type;
        return this;
    }

    public AircraftBuilder SetManufacturer(string manufacturer)
    {
        _manufacturer = manufacturer;
        return this;
    }

    public AircraftBuilder SetModel(string model)
    {
        _model = model;
        return this;
    }

    public AircraftBuilder SetSeatingCapacity(int seatingCapacity)
    {
        _seatingCapacity = seatingCapacity;
        return this;
    }

    public AircraftBuilder SetEconomyCostPerKM(int economyCostPerKM)
    {
        _economyCostPerKM = economyCostPerKM;
        return this;
    }

    public AircraftBuilder SetFirstClassCostPerKM(int firstClassCostPerKM)
    {
        _firstClassCostPerKM = firstClassCostPerKM;
        return this;
    }

    public AircraftBuilder SetRange(int range)
    {
        _range = range;
        return this;
    }

    public AircraftBuilder SetCruisingAltitude(int cruisingAltitude)
    {
        _cruisingAltitude = cruisingAltitude;
        return this;
    }

    public AircraftBuilder SetMaxTakeoffWeight(int maxTakeoffWeight)
    {
        _maxTakeoffWeight = maxTakeoffWeight;
        return this;
    }

    public AircraftBuilder SetLength(int length)
    {
        _length = length;
        return this;
    }

    public AircraftBuilder SetWingspan(int wingspan)
    {
        _wingspan = wingspan;
        return this;
    }

    public AircraftBuilder SetHeight(int height)
    {
        _height = height;
        return this;
    }

    public AircraftBuilder AddToEngine(Engine engine)
    {
        _engines.Add(engine);
        return this;
    }
}
