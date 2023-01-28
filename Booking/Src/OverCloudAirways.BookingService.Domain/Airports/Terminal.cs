using Newtonsoft.Json;
using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BookingService.Domain.Airports;

public class Terminal : ValueObject
{
    public string Name { get; }
    public IReadOnlyList<string> Gates { get; }
    public IReadOnlyList<string> Amenities { get; }

    private Terminal()
    {
    }

    [JsonConstructor]
    private Terminal(string name, IEnumerable<string> gates, IEnumerable<string> amenities)
    {
        Name = name;
        Gates = gates.ToList().AsReadOnly();
        Amenities = amenities.ToList().AsReadOnly();
    }

    public static Terminal Of(string name, IEnumerable<string> gates, IEnumerable<string> amenities)
    {
        return new Terminal(name, gates, amenities);
    }
}
