using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BookingService.Domain.Airports.Events;

namespace OverCloudAirways.BookingService.TestHelpers.Airports;

public class AirportCreatedDomainEventBuilder
{
    private AirportId _airportId = AirportId.New();
    private string _name = "John F. Kennedy International Airport";
    private string _code = "JFK";
    private string _location = "New York, USA";
    private List<Terminal> _terminals = new()
    {
        new TerminalBuilder().Build()
    };

    public AirportCreatedDomainEvent Build()
    {
        return new AirportCreatedDomainEvent(_airportId, _code, _name, _location, _terminals);
    }

    public AirportCreatedDomainEventBuilder SetId(AirportId id)
    {
        _airportId = id;
        return this;
    }

    public AirportCreatedDomainEventBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public AirportCreatedDomainEventBuilder SetCode(string code)
    {
        _code = code;
        return this;
    }

    public AirportCreatedDomainEventBuilder SetLocation(string location)
    {
        _location = location;
        return this;
    }

    public AirportCreatedDomainEventBuilder AddToTerminals(Terminal terminal)
    {
        _terminals.Add(terminal);
        return this;
    }
}
