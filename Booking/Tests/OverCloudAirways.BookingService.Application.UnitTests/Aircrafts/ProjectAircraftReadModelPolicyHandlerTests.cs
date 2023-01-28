using NSubstitute;
using OverCloudAirways.BookingService.Application.Aircrafts.Commands.ProjectReadModel;
using OverCloudAirways.BookingService.Application.Aircrafts.Policies.Created;
using OverCloudAirways.BookingService.TestHelpers.Aircrafts;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using Xunit;

namespace OverCloudAirways.BookingService.Application.UnitTests.Aircrafts;

public class ProjectAircraftReadModelPolicyHandlerTests
{
    [Fact]
    public async Task Handle_ShouldEnqueueProjectAircraftReadModelCommand()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new ProjectAircraftReadModelPolicyHandler(commandsScheduler);
        var notification = new AircraftCreatedPolicyBuilder().Build();

        // Act
        await handler.Handle(notification, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueueAsync(Arg.Is<ProjectAircraftReadModelCommand>(c => c.AircraftId == notification.DomainEvent.AircraftId));
    }
}
