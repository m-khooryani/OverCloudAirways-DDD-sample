using NSubstitute;
using OverCloudAirways.BookingService.Application.Aircrafts.Commands.ProjectReadModel;
using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BookingService.TestHelpers.Aircrafts;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;
using Xunit;

namespace OverCloudAirways.BookingService.Application.UnitTests.Aircrafts;

public class ProjectAircraftReadModelCommandTests
{
    [Fact]
    public async Task HandleAsync_GivenCommand_ShouldUpsertReadModel()
    {
        // Arrange
        var aggregateRepository = Substitute.For<IAggregateRepository>();
        var cosmosManager = Substitute.For<ICosmosManager>();
        var handler = new ProjectAircraftReadModelCommandHandler(cosmosManager, aggregateRepository);
        var aircraftId = AircraftId.New();
        var command = new ProjectAircraftReadModelCommand(aircraftId);
        var aggregate = new AircraftBuilder()
            .SetAircraftId(aircraftId)
            .Build();
        aggregateRepository.LoadAsync<Aircraft, AircraftId>(command.AircraftId).Returns(aggregate);

        // Act
        await handler.HandleAsync(command, CancellationToken.None);

        // Assert
        await aggregateRepository.Received(1).LoadAsync<Aircraft, AircraftId>(command.AircraftId);
        await cosmosManager.Received(1).UpsertAsync(ContainersConstants.ReadModels, Arg.Is<AircraftReadModel>(x =>
            x.AircraftId == aggregate.Id &&
            x.Type == aggregate.Type &&
            x.Manufacturer == aggregate.Manufacturer &&
            x.Model == aggregate.Model &&
            x.SeatingCapacity == aggregate.SeatingCapacity &&
            x.EconomyCostPerKM == aggregate.EconomyCostPerKM &&
            x.FirstClassCostPerKM == aggregate.FirstClassCostPerKM &&
            x.Range == aggregate.Range &&
            x.CruisingAltitude == aggregate.CruisingAltitude &&
            x.MaxTakeoffWeight == aggregate.MaxTakeoffWeight &&
            x.Length == aggregate.Length &&
            x.Wingspan == aggregate.Wingspan &&
            x.Height == aggregate.Height &&
            x.Engines.SequenceEqual(aggregate.Engines)
        ));
    }
}
