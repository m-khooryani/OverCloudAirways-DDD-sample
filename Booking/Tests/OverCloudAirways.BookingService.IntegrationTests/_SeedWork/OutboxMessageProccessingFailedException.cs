namespace OverCloudAirways.BookingService.IntegrationTests._SeedWork;

class OutboxMessageProccessingFailedException : Exception
{
    public OutboxMessageProccessingFailedException(string message)
        : base(message)
    {
    }
}
