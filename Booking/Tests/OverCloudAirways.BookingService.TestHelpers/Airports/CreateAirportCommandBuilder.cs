using OverCloudAirways.BookingService.Application.Airports.Commands.Create;
using OverCloudAirways.BookingService.Domain.Airports;

namespace OverCloudAirways.BookingService.TestHelpers.Airports;

public class CreateAirportCommandBuilder
{
    private AirportId _airportId = AirportId.New();
    private string _name = "John F. Kennedy International Airport";
    private string _code = "JFK";
    private string _location = "New York, USA";
    private List<Terminal> _terminals = new()
    {
        new TerminalBuilder().Build()
    };

    public CreateAirportCommand Build()
    {
        return new CreateAirportCommand(_airportId, _code, _name, _location, _terminals);
    }

    public CreateAirportCommandBuilder SetId(AirportId id)
    {
        _airportId = id;
        return this;
    }

    public CreateAirportCommandBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public CreateAirportCommandBuilder SetCode(string code)
    {
        _code = code;
        return this;
    }

    public CreateAirportCommandBuilder SetLocation(string location)
    {
        _location = location;
        return this;
    }

    public CreateAirportCommandBuilder AddToTerminals(Terminal terminal)
    {
        _terminals.Add(terminal);
        return this;
    }
}
