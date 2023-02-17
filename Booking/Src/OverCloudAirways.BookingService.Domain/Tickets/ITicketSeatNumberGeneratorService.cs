namespace OverCloudAirways.BookingService.Domain.Tickets;

public interface ITicketSeatNumberGeneratorService
{
    Task<string> GenerateAsync();
}
