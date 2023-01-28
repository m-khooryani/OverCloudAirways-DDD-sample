using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using OverCloudAirways.BookingService.Application.Aircrafts.Commands.ProjectReadModel;
using OverCloudAirways.BookingService.Application.Airports.Commands.ProjectReadModel;
using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BookingService.TestHelpers.Aircrafts;
using OverCloudAirways.BookingService.TestHelpers.Airports;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;
using Xunit;

namespace OverCloudAirways.BookingService.Application.UnitTests.Airports;

public class ProjectAirportReadModelCommandTests
{
    [Fact]
    public async Task HandleAsync_GivenCommand_ShouldUpsertReadModel()
    {
        // Arrange
        // IAirportCodeUniqueChecker
        var uniqueCodeChecker = Substitute.For<IAirportCodeUniqueChecker>();
        uniqueCodeChecker.IsUniqueAsync(Arg.Any<string>()).Returns(true);

        // Airport
        var aggregate = await new AirportBuilder()
            .SetAirportCodeUniqueChecker(uniqueCodeChecker)
            .BuildAsync();

        // IAggregateRepository
        var command = new ProjectAirportReadModelCommand(AirportId.New());
        var aggregateRepository = Substitute.For<IAggregateRepository>();
        aggregateRepository.LoadAsync<Airport, AirportId>(command.AirportId).Returns(aggregate);
        
        // Command Handler
        var cosmosManager = Substitute.For<ICosmosManager>();
        var handler = new ProjectAirportReadModelCommandHandler(cosmosManager, aggregateRepository);

        // Act
        await handler.HandleAsync(command, CancellationToken.None);

        // Assert
        await aggregateRepository.Received(1).LoadAsync<Airport, AirportId>(command.AirportId);
        await cosmosManager.Received(1).UpsertAsync(ContainersConstants.ReadModels, Arg.Is<AirportReadModel>(x =>
            x.Id == aggregate.Id.Value &&
            x.Code == aggregate.Code &&
            x.Name == aggregate.Name &&
            x.Location == aggregate.Location &&
            x.Terminals.SequenceEqual(aggregate.Terminals)
        ));
    }
}
