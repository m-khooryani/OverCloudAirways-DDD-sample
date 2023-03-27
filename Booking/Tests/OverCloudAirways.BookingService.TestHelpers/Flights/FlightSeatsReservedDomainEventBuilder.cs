using OverCloudAirways.BookingService.Domain._Shared;
using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BookingService.Domain.Flights.Events;
using OverCloudAirways.BookingService.TestHelpers._Shared;

namespace OverCloudAirways.BookingService.TestHelpers.Flights;

public class FlightSeatsReservedDomainEventBuilder
{
    private FlightId _flightId = FlightId.New();
    private CustomerId _customerId = CustomerId.New();
    private List<Passenger> _passengers = new List<Passenger>()
    {
        new PassengerBuilder().Build(),
        new PassengerBuilder().Build()
    };
    private int _seatsCount = 2;

    public FlightSeatsReservedDomainEvent Build()
    {
        return new FlightSeatsReservedDomainEvent(_flightId, _customerId, _passengers);
    }

    public FlightSeatsReservedDomainEventBuilder SetFlightId(FlightId flightId)
    {
        _flightId = flightId;
        return this;
    }

    public FlightSeatsReservedDomainEventBuilder SetCustomerId(CustomerId customerId)
    {
        _customerId = customerId;
        return this;
    }

    public FlightSeatsReservedDomainEventBuilder SetSeatsCount(int seatsCount)
    {
        _seatsCount = seatsCount;
        return this;
    }
}
