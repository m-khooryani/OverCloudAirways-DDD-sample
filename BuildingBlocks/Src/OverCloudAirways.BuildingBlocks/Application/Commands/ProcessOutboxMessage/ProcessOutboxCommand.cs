namespace OverCloudAirways.BuildingBlocks.Application.Commands.ProcessOutboxMessage;

public record ProcessOutboxCommand : Command
{
    public ProcessOutboxCommand(string outboxMessageId)
    {
        MessageId = outboxMessageId;
    }

    public string MessageId { get; }
}
