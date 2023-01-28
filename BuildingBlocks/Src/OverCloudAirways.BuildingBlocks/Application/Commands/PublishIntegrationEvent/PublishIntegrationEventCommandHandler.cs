using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BuildingBlocks.Application.Commands.PublishIntegrationEvent;

internal class PublishIntegrationEventCommandHandler : CommandHandler<PublishIntegrationEventCommand>
{
    private readonly IEventBus _eventBus;

    public PublishIntegrationEventCommandHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public override async Task HandleAsync(PublishIntegrationEventCommand command, CancellationToken cancellationToken)
    {
        await _eventBus.PublishAsync(command.IntegrationEvent);
    }
}