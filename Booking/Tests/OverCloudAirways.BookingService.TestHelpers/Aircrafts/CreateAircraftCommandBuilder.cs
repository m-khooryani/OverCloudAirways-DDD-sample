using OverCloudAirways.BookingService.Application.Aircrafts.Commands.Create;
using OverCloudAirways.BookingService.Domain.Aircrafts;

namespace OverCloudAirways.BookingService.TestHelpers.Aircrafts;

public class CreateAircraftCommandBuilder
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

    public CreateAircraftCommand Build()
    {
        return new CreateAircraftCommand(
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

    public CreateAircraftCommandBuilder SetAircraftId(AircraftId aircraftId)
    {
        _aircraftId = aircraftId;
        return this;
    }

    public CreateAircraftCommandBuilder SetType(string type)
    {
        _type = type;
        return this;
    }

    public CreateAircraftCommandBuilder SetManufacturer(string manufacturer)
    {
        _manufacturer = manufacturer;
        return this;
    }

    public CreateAircraftCommandBuilder SetModel(string model)
    {
        _model = model;
        return this;
    }

    public CreateAircraftCommandBuilder SetSeatingCapacity(int seatingCapacity)
    {
        _seatingCapacity = seatingCapacity;
        return this;
    }

    public CreateAircraftCommandBuilder SetEconomyCostPerKM(int economyCostPerKM)
    {
        _economyCostPerKM = economyCostPerKM;
        return this;
    }

    public CreateAircraftCommandBuilder SetFirstClassCostPerKM(int firstClassCostPerKM)
    {
        _firstClassCostPerKM = firstClassCostPerKM;
        return this;
    }

    public CreateAircraftCommandBuilder SetRange(int range)
    {
        _range = range;
        return this;
    }

    public CreateAircraftCommandBuilder SetCruisingAltitude(int cruisingAltitude)
    {
        _cruisingAltitude = cruisingAltitude;
        return this;
    }

    public CreateAircraftCommandBuilder SetMaxTakeoffWeight(int maxTakeoffWeight)
    {
        _maxTakeoffWeight = maxTakeoffWeight;
        return this;
    }

    public CreateAircraftCommandBuilder SetLength(int length)
    {
        _length = length;
        return this;
    }

    public CreateAircraftCommandBuilder SetWingspan(int wingspan)
    {
        _wingspan = wingspan;
        return this;
    }

    public CreateAircraftCommandBuilder SetHeight(int height)
    {
        _height = height;
        return this;
    }

    public CreateAircraftCommandBuilder AddToEngine(Engine engine)
    {
        _engines.Add(engine);
        return this;
    }
}