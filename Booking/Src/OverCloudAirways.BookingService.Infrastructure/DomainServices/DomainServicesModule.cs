using Autofac;
using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BookingService.Domain.Tickets;
using OverCloudAirways.BookingService.Infrastructure.DomainServices.Airports;
using OverCloudAirways.BookingService.Infrastructure.DomainServices.Flights;
using OverCloudAirways.BookingService.Infrastructure.DomainServices.Tickets;

namespace OverCloudAirways.BookingService.Infrastructure.DomainServices;

public class DomainServicesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<AirportCodeUniqueChecker>()
            .As<IAirportCodeUniqueChecker>()
            .SingleInstance();

        builder
            .RegisterType<FlightPriceCalculatorService>()
            .As<IFlightPriceCalculatorService>()
            .SingleInstance();

        builder
            .RegisterType<TicketSeatNumberGeneratorService>()
            .As<ITicketSeatNumberGeneratorService>()
            .SingleInstance();
    }
}
