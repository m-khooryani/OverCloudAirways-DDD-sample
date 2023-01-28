using OverCloudAirways.BookingService.Domain.Airports;

namespace OverCloudAirways.BookingService.TestHelpers.Airports;

public class AirportBuilder
{
    private IAirportCodeUniqueChecker _codeUniqueChecker = null;
    private AirportId _airportId = AirportId.New();
    private string _name = "John F. Kennedy International Airport";
    private string _code = "JFK";
    private string _location = "New York, USA";
    private List<Terminal> _terminals = new()
    {
        new TerminalBuilder().Build()
    };

    public async Task<Airport> BuildAsync()
    {
        return await Airport.CreateAsync(_codeUniqueChecker, _airportId, _code, _name, _location, _terminals);
    }

    public AirportBuilder SetAirportCodeUniqueChecker(IAirportCodeUniqueChecker codeUniqueChecker)
    {
        _codeUniqueChecker = codeUniqueChecker;
        return this;
    }

    public AirportBuilder SetId(AirportId id)
    {
        _airportId = id;
        return this;
    }

    public AirportBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public AirportBuilder SetCode(string code)
    {
        _code = code;
        return this;
    }

    public AirportBuilder SetLocation(string location)
    {
        _location = location;
        return this;
    }

    public AirportBuilder AddToTerminals(Terminal terminal)
    {
        _terminals.Add(terminal);
        return this;
    }
}
