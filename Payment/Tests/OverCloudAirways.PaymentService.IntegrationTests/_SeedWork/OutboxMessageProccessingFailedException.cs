namespace OverCloudAirways.PaymentService.IntegrationTests._SeedWork;

class OutboxMessageProccessingFailedException : Exception
{
    public OutboxMessageProccessingFailedException(string message)
        : base(message)
    {
    }
}
