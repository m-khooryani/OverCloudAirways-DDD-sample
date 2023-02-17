using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BookingService.Domain.Tickets;
using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;

namespace OverCloudAirways.BookingService.Application.Tickets.Commands.ProjectReadModel;

internal class ProjectTicketReadModelCommandHandler : CommandHandler<ProjectTicketReadModelCommand>
{
    private readonly ICosmosManager _cosmosManager;
    private readonly IAggregateRepository _aggregateRepository;

    public ProjectTicketReadModelCommandHandler(
        ICosmosManager cosmosManager,
        IAggregateRepository aggregateRepository)
    {
        _cosmosManager = cosmosManager;
        _aggregateRepository = aggregateRepository;
    }

    public override async Task HandleAsync(ProjectTicketReadModelCommand command, CancellationToken cancellationToken)
    {
        var ticket = await _aggregateRepository.LoadAsync<Ticket, TicketId>(command.TicketId);
        var flight = await _aggregateRepository.LoadAsync<Flight, FlightId>(ticket.FlightId);
        var customer = await _aggregateRepository.LoadAsync<Customer, CustomerId>(ticket.CustomerId);
        var departureAirport = await _aggregateRepository.LoadAsync<Airport, AirportId>(flight.DepartureAirportId);
        var destinationAirport = await _aggregateRepository.LoadAsync<Airport, AirportId>(flight.DestinationAirportId);

        var readmodel = new TicketReadModel(
            ticket.Id.Value,
            customer.FirstName,
            customer.LastName,
            flight.Number,
            flight.DepartureTime,
            flight.ArrivalTime,
            departureAirport.Code,
            destinationAirport.Code);

        await _cosmosManager.UpsertAsync(ContainersConstants.ReadModels, readmodel);
    }
}
