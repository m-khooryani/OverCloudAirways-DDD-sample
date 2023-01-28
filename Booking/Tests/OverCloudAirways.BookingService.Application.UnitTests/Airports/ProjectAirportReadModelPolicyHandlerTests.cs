using NSubstitute;
using OverCloudAirways.BookingService.Application.Airports.Commands.ProjectReadModel;
using OverCloudAirways.BookingService.Application.Airports.Policies.Created;
using OverCloudAirways.BookingService.TestHelpers.Airports;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using Xunit;

namespace OverCloudAirways.BookingService.Application.UnitTests.Airports;

public class ProjectAirportReadModelPolicyHandlerTests
{
    [Fact]
    public async Task Handle_ShouldEnqueueProjectAirportReadModelCommand()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new ProjectAirportReadModelPolicyHandler(commandsScheduler);
        var notification = new AirportCreatedPolicyBuilder().Build();

        // Act
        await handler.Handle(notification, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueueAsync(Arg.Is<ProjectAirportReadModelCommand>(c => c.AirportId == notification.DomainEvent.AirportId));
    }
}
