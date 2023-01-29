using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Application.Flights.Commands.Schedule;

internal class ScheduleFlightCommandHandler : CommandHandler<ScheduleFlightCommand>
{
    private readonly IAggregateRepository _aggregateRepository;

    public ScheduleFlightCommandHandler(IAggregateRepository aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
    }

    public override async Task HandleAsync(ScheduleFlightCommand command, CancellationToken cancellationToken)
    {
        var flight = await Flight.ScheduleAsync(
            _aggregateRepository,
            command.FlightId,
            command.Number,
            command.DepartureAirportId,
            command.DestinationAirportId,
            command.DepartureTime,
            command.ArrivalTime,
            command.Route,
            command.Distance,
            command.AircraftId,
            command.AvailableSeats,
            command.BookedSeats,
            command.MaximumLuggageWeight);

        _aggregateRepository.Add(flight);
    }
}