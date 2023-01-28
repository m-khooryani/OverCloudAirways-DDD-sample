namespace OverCloudAirways.BookingService.Domain.Airports;

public interface IAirportCodeUniqueChecker
{
    Task<bool> IsUniqueAsync(string code);
}