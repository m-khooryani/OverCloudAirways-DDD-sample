﻿using NSubstitute;
using OverCloudAirways.BookingService.Application.Flights.Commands.ProjectReadModel;
using OverCloudAirways.BookingService.Application.Flights.Policies.PriceCharged;
using OverCloudAirways.BookingService.TestHelpers.Flights;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using Xunit;

namespace OverCloudAirways.BookingService.Application.UnitTests.Flights;

public class EnqueueProjectingFlightReadModelPolicyHandlerTests
{
    [Fact]
    public async Task Handle_Should_Enqueue_ProjectFlightReadModelCommand()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new EnqueueProjectingFlightReadModelPolicyHandler(commandsScheduler);
        var policy = new FlightPriceChargedPolicyBuilder().Build();

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueueAsync(Arg.Is<ProjectFlightReadModelCommand>(c => c.FlightId == policy.DomainEvent.FlightId));
    }
}