using OverCloudAirways.BookingService.Domain.Tickets;

namespace OverCloudAirways.BookingService.Infrastructure.DomainServices.Tickets;

internal class TicketSeatNumberGeneratorService : ITicketSeatNumberGeneratorService
{
    private readonly ThreadLocal<Random> _random;

    public TicketSeatNumberGeneratorService()
    {
        _random = new ThreadLocal<Random>(() => new Random());
    }

    public Task<string> GenerateAsync()
    {
        char row = (char)_random.Value.Next('A', 'Z' + 1);
        int seatNumber = _random.Value.Next(10, 100);
        string seat = $"{row}{seatNumber}";

        return Task.FromResult(seat);
    }
}
