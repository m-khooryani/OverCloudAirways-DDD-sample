using OverCloudAirways.BookingService.Domain.Airports.Events;
using OverCloudAirways.BookingService.Domain.Airports.Rules;
using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BookingService.Domain.Airports;

public class Airport : AggregateRoot<AirportId>
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string Location { get; private set; }
    private List<Terminal> _terminals;

    public IReadOnlyCollection<Terminal> Terminals => _terminals.AsReadOnly();

    private Airport()
	{
	}

    public static async Task<Airport> CreateAsync(
        IAirportCodeUniqueChecker codeChecker,
        AirportId id,
        string code,
        string name,
        string location,
        IReadOnlyList<Terminal> terminals)
    {
        await CheckRuleAsync(new AirportCodeShouldBeUniqueRule(code, codeChecker));

        var @event = new AirportCreatedDomainEvent(
            id,
            code,
            name,
            location,
            terminals);

        var airport = new Airport();
        airport.Apply(@event);

        return airport;
    }

    protected void When(AirportCreatedDomainEvent @event)
    {
        Id = @event.AirportId;
        Code = @event.Code;
        Name = @event.Name;
        Location = @event.Location;
        _terminals = @event.Terminals.ToList();
    }
}
