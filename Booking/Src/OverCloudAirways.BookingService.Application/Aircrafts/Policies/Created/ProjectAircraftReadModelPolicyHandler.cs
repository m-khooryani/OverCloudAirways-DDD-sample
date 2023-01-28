using OverCloudAirways.BookingService.Application.Aircrafts.Commands.ProjectReadModel;
using OverCloudAirways.BookingService.Domain.Aircrafts.Events;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Application.Aircrafts.Policies.Created;

internal class ProjectAircraftReadModelPolicyHandler : IDomainPolicyHandler<AircraftCreatedPolicy, AircraftCreatedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public ProjectAircraftReadModelPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(AircraftCreatedPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ProjectAircraftReadModelCommand(notification.DomainEvent.AircraftId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}

