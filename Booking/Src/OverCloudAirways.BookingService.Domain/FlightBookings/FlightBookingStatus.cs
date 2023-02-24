using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BookingService.Domain.FlightBookings;

public abstract class FlightBookingStatus : Enumeration
{
    public static readonly FlightBookingStatus None = new NoneBookingStatus();
    public static readonly FlightBookingStatus Reserved = new ReservedBookingStatus();
    public static readonly FlightBookingStatus Confirmed = new ConfirmedBookingStatus();
    public static readonly FlightBookingStatus Cancelled = new CancelledBookingStatus();

    private FlightBookingStatus(int value, string name) : base(value, name)
    {
    }

    private class NoneBookingStatus : FlightBookingStatus
    {
        public NoneBookingStatus() : base(0, "None")
        {
        }
    }

    private class ReservedBookingStatus : FlightBookingStatus
    {
        public ReservedBookingStatus() : base(1, "Reserved")
        {
        }
    }

    private class ConfirmedBookingStatus : FlightBookingStatus
    {
        public ConfirmedBookingStatus() : base(2, "Confirmed")
        {
        }
    }

    private class CancelledBookingStatus : FlightBookingStatus
    {
        public CancelledBookingStatus() : base(3, "Cancelled")
        {
        }
    }
}
