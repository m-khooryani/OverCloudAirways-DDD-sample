using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BuildingBlocks.Application.Commands.PublishIntegrationEvent;

internal record PublishIntegrationEventCommand(
    IntegrationEvent IntegrationEvent) : Command;
