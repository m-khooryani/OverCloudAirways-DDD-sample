using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;

namespace OverCloudAirways.BookingService.Application.Airports.Commands.ProjectReadModel;

internal class ProjectAirportReadModelCommandHandler : CommandHandler<ProjectAirportReadModelCommand>
{
    private readonly ICosmosManager _cosmosManager;
    private readonly IAggregateRepository _aggregateRepository;

    public ProjectAirportReadModelCommandHandler(
        ICosmosManager cosmosManager,
        IAggregateRepository aggregateRepository)
    {
        _cosmosManager = cosmosManager;
        _aggregateRepository = aggregateRepository;
    }

    public override async Task HandleAsync(ProjectAirportReadModelCommand command, CancellationToken cancellationToken)
    {
        var aggregate = await _aggregateRepository.LoadAsync<Airport, AirportId>(command.AirportId);
        var readmodel = new AirportReadModel(
            aggregate.Id.Value,
            aggregate.Code,
            aggregate.Name,
            aggregate.Location,
            aggregate.Terminals);

        await _cosmosManager.UpsertAsync(ContainersConstants.ReadModels, readmodel);
    }
}
