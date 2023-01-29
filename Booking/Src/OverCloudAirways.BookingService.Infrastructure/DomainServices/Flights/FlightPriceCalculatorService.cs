using OverCloudAirways.BookingService.Domain.Flights;

namespace OverCloudAirways.BookingService.Infrastructure.DomainServices.Flights;

internal class FlightPriceCalculatorService : IFlightPriceCalculatorService
{
    public Task<(decimal economyPrice, decimal firstClassPrice)> CalculateAsync(Flight _)
    {
        // chosed a dummy calculation for simplicity
        // calculate based on factors, or calling a third party service 
        // event it could be possible to listen to an integration event
        return Task.FromResult((500M, 1000M));
    }
}
