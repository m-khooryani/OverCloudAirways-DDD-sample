using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BookingService.Domain.Flights.Events;

namespace OverCloudAirways.BookingService.TestHelpers.Flights;

public class FlightPriceChargedDomainEventBuilder
{
    private FlightId _flightId = FlightId.New();
    private decimal _economyPrice = 100M;
    private decimal _firstClassPrice = 200M;

    public FlightPriceChargedDomainEvent Build()
    {
        return new FlightPriceChargedDomainEvent(_flightId, _economyPrice, _firstClassPrice);
    }

    public FlightPriceChargedDomainEventBuilder SetFlightId(FlightId flightId)
    {
        _flightId = flightId;
        return this;
    }

    public FlightPriceChargedDomainEventBuilder SetEconomyPrice(decimal economyPrice)
    {
        _economyPrice = economyPrice;
        return this;
    }

    public FlightPriceChargedDomainEventBuilder SetFirstClassPrice(decimal firstClassPrice)
    {
        _firstClassPrice = firstClassPrice;
        return this;
    }
}
