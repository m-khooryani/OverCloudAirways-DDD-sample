using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BuildingBlocks.Application.Commands.PublishIntegrationEvent;

public record PublishIntegrationEventCommand(
    IntegrationEvent IntegrationEvent) : Command;
