using OverCloudAirways.BookingService.Domain.Airports;

namespace OverCloudAirways.BookingService.TestHelpers.Airports;

public class TerminalBuilder
{
    private string _name = "Terminal 1";
    private List<string> _gates = new()
    {
        "A1", "A2", "A3"
    };
    private List<string> _amenities = new()
    {
        "Food court", "Shopping", "Lounge"
    };

    public Terminal Build()
    {
        return Terminal.Of(_name, _gates, _amenities);
    }

    public TerminalBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public TerminalBuilder AddToGates(string gate)
    {
        _gates.Add(gate);
        return this;
    }

    public TerminalBuilder AddToAmenities(string amenity) 
    {
        _amenities.Add(amenity);
        return this;
    }
}