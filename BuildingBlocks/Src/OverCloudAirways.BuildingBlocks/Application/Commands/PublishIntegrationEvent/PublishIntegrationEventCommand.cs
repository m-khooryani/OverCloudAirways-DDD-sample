using DArch.Application.Contracts;
using DArch.Infrastructure.EventBus;

namespace OverCloudAirways.BuildingBlocks.Application.Commands.PublishIntegrationEvent;

public record PublishIntegrationEventCommand(
    IntegrationEvent IntegrationEvent) : Command;
