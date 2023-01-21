using DArch.Application.Contracts;

namespace DArch.Infrastructure.Configuration.Processing.Outbox;

public record ProcessOutboxCommand : Command
{
    public ProcessOutboxCommand(string outboxMessageId)
    {
        MessageId = outboxMessageId;
    }

    public string MessageId { get; }
}
