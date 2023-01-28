using FluentValidation;
using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Application.Aircrafts.Commands.Create;

internal class CreateAircraftCommandHandler : CommandHandler<CreateAircraftCommand>
{
    private readonly IAggregateRepository _aggregateRepository;

    public CreateAircraftCommandHandler(IAggregateRepository aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
    }

    public override Task HandleAsync(CreateAircraftCommand command, CancellationToken cancellationToken)
    {
        var aircraft = Aircraft.Create(
            command.AircraftId,
            command.Type,
            command.Manufacturer,
            command.Model,
            command.SeatingCapacity,
            command.EconomyCostPerKM,
            command.FirstClassCostPerKM,
            command.Range,
            command.CruisingAltitude,
            command.MaxTakeoffWeight,
            command.Length,
            command.Wingspan,
            command.Height,
            command.Engines);

        _aggregateRepository.Add<Aircraft, AircraftId>(aircraft);

        return Task.CompletedTask;
    }
}
