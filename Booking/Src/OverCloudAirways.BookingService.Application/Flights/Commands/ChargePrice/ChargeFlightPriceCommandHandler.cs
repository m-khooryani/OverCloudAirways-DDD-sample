using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Application.Flights.Commands.ChargePrice;

internal class ChargeFlightPriceCommandHandler : CommandHandler<ChargeFlightPriceCommand>
{
    private readonly IAggregateRepository _aggregateRepository;
    private readonly IFlightPriceCalculatorService _priceCalculatorService;

    public ChargeFlightPriceCommandHandler(
        IAggregateRepository aggregateRepository, 
        IFlightPriceCalculatorService priceCalculatorService)
    {
        _aggregateRepository = aggregateRepository;
        _priceCalculatorService = priceCalculatorService;
    }

    public override async Task HandleAsync(ChargeFlightPriceCommand command, CancellationToken cancellationToken)
    {
        var flight = await _aggregateRepository.LoadAsync<Flight, FlightId>(command.FlightId);

        await flight.ChargePriceAsync(_priceCalculatorService);
    }
}
