using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;

namespace OverCloudAirways.BookingService.Application.Flights.Commands.ProjectReadModel;

internal class ProjectFlightReadModelCommandHandler : CommandHandler<ProjectFlightReadModelCommand>
{
    private readonly ICosmosManager _cosmosManager;
    private readonly IAggregateRepository _aggregateRepository;

    public ProjectFlightReadModelCommandHandler(
        ICosmosManager cosmosManager,
        IAggregateRepository aggregateRepository)
    {
        _cosmosManager = cosmosManager;
        _aggregateRepository = aggregateRepository;
    }

    public override async Task HandleAsync(ProjectFlightReadModelCommand command, CancellationToken cancellationToken)
    {
        var flight = await _aggregateRepository.LoadAsync<Flight, FlightId>(command.FlightId);
        var departureAirport = await _aggregateRepository.LoadAsync<Airport, AirportId>(flight.DepartureAirportId);
        var destinationAirport = await _aggregateRepository.LoadAsync<Airport, AirportId>(flight.DestinationAirportId);
        var aircraft = await _aggregateRepository.LoadAsync<Aircraft, AircraftId>(flight.AircraftId);

        var readmodel = new FlightReadModel(
            flight.Id.Value,
            flight.Number,
            departureAirport.Code,
            destinationAirport.Code,
            flight.DepartureTime,
            flight.ArrivalTime,
            flight.Route,
            flight.Distance,
            aircraft.Model,
            flight.AvailableSeats,
            flight.BookedSeats,
            flight.MaximumLuggageWeight,
            flight.EconomyPrice!.Value,
            flight.FirstClassPrice!.Value);

        await _cosmosManager.UpsertAsync(ContainersConstants.ReadModels, readmodel);
    }
}