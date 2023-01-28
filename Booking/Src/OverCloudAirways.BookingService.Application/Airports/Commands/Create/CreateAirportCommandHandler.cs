using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Application.Airports.Commands.Create;

internal class CreateAirportCommandHandler : CommandHandler<CreateAirportCommand>
{
    private readonly IAggregateRepository _aggregateRepository;
    private readonly IAirportCodeUniqueChecker _codeUniqueChecker;

    public CreateAirportCommandHandler(
        IAggregateRepository aggregateRepository, 
        IAirportCodeUniqueChecker codeUniqueChecker)
    {
        _aggregateRepository = aggregateRepository;
        _codeUniqueChecker = codeUniqueChecker;
    }

    public override async Task HandleAsync(CreateAirportCommand command, CancellationToken cancellationToken)
    {
        var airport = await Airport.CreateAsync(
            _codeUniqueChecker,
            command.AirportId,
            command.Code,
            command.Name,
            command.Location,
            command.Terminals);

        _aggregateRepository.Add(airport);
    }
}
