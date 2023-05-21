namespace OverCloudAirways.BuildingBlocks.Application.Commands.ProcessOutboxMessage;

public record ProcessOutboxCommand : Command
{
    public ProcessOutboxCommand(Guid outboxMessageId)
    {
        MessageId = outboxMessageId;
    }

    public Guid MessageId { get; }
}
