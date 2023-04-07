using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BookingService.Domain.Aircrafts;

public class Engine : ValueObject
{
    public string Type { get; }
    public int Thrust { get; }
    public int Diameter { get; }
    public int DryWeight { get; }

    private Engine()
    { 
    }

    private Engine(string type, int thrust, int diameter, int dryWeight)
    {
        Type = type;
        Thrust = thrust;
        Diameter = diameter;
        DryWeight = dryWeight;
    }

    public static Engine Of(string type, int thrust, int diameter, int dryWeight)
    {
        return new Engine(type, thrust, diameter, dryWeight);
    }
}