namespace OverCloudAirways.BookingService.Domain.Flights;

public interface IFlightPriceCalculatorService
{
    Task<(decimal economyPrice, decimal firstClassPrice)> CalculateAsync(Flight flight);
}
