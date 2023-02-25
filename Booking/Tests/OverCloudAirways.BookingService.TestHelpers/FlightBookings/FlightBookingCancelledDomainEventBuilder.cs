using OverCloudAirways.BookingService.Domain.FlightBookings.Events;
using OverCloudAirways.BookingService.Domain.FlightBookings;
using OverCloudAirways.BookingService.Domain.Flights;

namespace OverCloudAirways.BookingService.TestHelpers.FlightBookings;

public class FlightBookingCancelledDomainEventBuilder
{
    private FlightBookingId _flightBookingId = FlightBookingId.New();
    private FlightId _flightId = FlightId.New();
    private int _seatsCount = 2;

    public FlightBookingCancelledDomainEvent Build()
    {
        return new FlightBookingCancelledDomainEvent(_flightBookingId, _flightId, _seatsCount);
    }

    public FlightBookingCancelledDomainEventBuilder SetFlightBookingId(FlightBookingId flightBookingId)
    {
        _flightBookingId = flightBookingId;
        return this;
    }

    public FlightBookingCancelledDomainEventBuilder SetFlightId(FlightId flightId)
    {
        _flightId = flightId;
        return this;
    }

    public FlightBookingCancelledDomainEventBuilder SetSeatsCount(int seatsCount)
    {
        _seatsCount = seatsCount;
        return this;
    }
}
