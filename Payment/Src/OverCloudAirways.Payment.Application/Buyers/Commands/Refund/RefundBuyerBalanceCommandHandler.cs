using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Domain.Buyers;

namespace OverCloudAirways.PaymentService.Application.Buyers.Commands.Refund;

internal class RefundBuyerBalanceCommandHandler : CommandHandler<RefundBuyerBalanceCommand>
{
    private readonly IAggregateRepository _aggregateRepository;

    public RefundBuyerBalanceCommandHandler(IAggregateRepository aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
    }

    public override async Task HandleAsync(RefundBuyerBalanceCommand command, CancellationToken cancellationToken)
    {
        var buyer = await _aggregateRepository.LoadAsync<Buyer, BuyerId>(command.BuyerId);

        buyer.Refund(command.Amount);
    }
}