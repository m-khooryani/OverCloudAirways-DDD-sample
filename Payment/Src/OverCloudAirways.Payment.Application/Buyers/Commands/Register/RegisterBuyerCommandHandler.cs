using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Domain.Buyers;

namespace OverCloudAirways.PaymentService.Application.Buyers.Commands.Register;

internal class RegisterBuyerCommandHandler : CommandHandler<RegisterBuyerCommand>
{
    private readonly IAggregateRepository _aggregateRepository;

    public RegisterBuyerCommandHandler(IAggregateRepository aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
    }

    public override Task HandleAsync(RegisterBuyerCommand command, CancellationToken cancellationToken)
    {
        var buyer = Buyer.Register(
            command.BuyerId,
            command.FirstName,
            command.LastName,
            command.Email,
            command.PhoneNumber);

        _aggregateRepository.Add(buyer);

        return Task.CompletedTask;
    }
}