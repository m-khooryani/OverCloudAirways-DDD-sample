using OverCloudAirways.BookingService.Application.Airports.Commands.ProjectReadModel;
using OverCloudAirways.BookingService.Domain.Airports.Events;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Application.Airports.Policies.Created;

internal class EnqueueProjectingAirportReadModelPolicyHandler : IDomainPolicyHandler<AirportCreatedPolicy, AirportCreatedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public EnqueueProjectingAirportReadModelPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(AirportCreatedPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ProjectAirportReadModelCommand(notification.DomainEvent.AirportId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}

