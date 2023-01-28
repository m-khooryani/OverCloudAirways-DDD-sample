using Autofac;
using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BookingService.Infrastructure.DomainServices.Airports;

namespace OverCloudAirways.BookingService.Infrastructure.DomainServices;

public class DomainServicesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<AirportCodeUniqueChecker>()
            .As<IAirportCodeUniqueChecker>()
            .SingleInstance();
    }
}
