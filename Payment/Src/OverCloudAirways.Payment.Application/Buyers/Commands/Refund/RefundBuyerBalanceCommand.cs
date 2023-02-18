using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.PaymentService.Domain.Buyers;

namespace OverCloudAirways.PaymentService.Application.Buyers.Commands.Refund;

public record RefundBuyerBalanceCommand(
    BuyerId BuyerId,
    decimal Amount) : Command;
