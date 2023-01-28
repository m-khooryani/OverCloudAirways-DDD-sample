using OverCloudAirways.BookingService.Domain.Aircrafts;

public class EngineBuilder
{
    private string _type = "CF6-80C2B1F";
    private int _thrust = 60000;
    private int _diameter = 11;
    private int _dryWeight = 13500;

    public Engine Build()
    {
        return Engine.Of(_type, _thrust, _diameter, _dryWeight);
    }

    public EngineBuilder SetType(string type)
    {
        _type = type;
        return this;
    }

    public EngineBuilder SetThrust(int thrust)
    {
        _thrust = thrust;
        return this;
    }

    public EngineBuilder SetDiameter(int diameter)
    {
        _diameter = diameter;
        return this;
    }

    public EngineBuilder SetDryWeight(int dryWeight)
    {
        _dryWeight = dryWeight;
        return this;
    }
}
