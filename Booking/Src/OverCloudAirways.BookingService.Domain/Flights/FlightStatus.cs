using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BookingService.Domain.Flights;

public abstract class FlightStatus : Enumeration
{
    public static readonly FlightStatus None = new NoneFlightStatus();
    public static readonly FlightStatus Scheduled = new ScheduledFlightStatus();
    public static readonly FlightStatus Cancelled = new CancelledFlightStatus();
    public static readonly FlightStatus Departed = new DepartedFlightStatus();
    public static readonly FlightStatus Arrived = new ArrivedFlightStatus();

    private FlightStatus(int value, string name) : base(value, name)
    {
    }

    internal bool HasNotYetDeparted()
    {
        return this != None && this == Scheduled;
    }

    private class NoneFlightStatus : FlightStatus
    {
        public NoneFlightStatus() : base(0, "None")
        {
        }
    }

    private class ScheduledFlightStatus : FlightStatus
    {
        public ScheduledFlightStatus() : base(1, "Scheduled")
        {
        }
    }

    private class CancelledFlightStatus : FlightStatus
    {
        public CancelledFlightStatus() : base(2, "Cancelled")
        {
        }
    }

    private class DepartedFlightStatus : FlightStatus
    {
        public DepartedFlightStatus() : base(3, "Departed")
        {
        }
    }

    private class ArrivedFlightStatus : FlightStatus
    {
        public ArrivedFlightStatus() : base(4, "Arrived")
        {
        }
    }
}
