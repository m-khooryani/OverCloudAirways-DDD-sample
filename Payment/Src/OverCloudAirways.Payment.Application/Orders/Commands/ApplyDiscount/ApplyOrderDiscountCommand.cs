using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.PaymentService.Domain.Orders;
using OverCloudAirways.PaymentService.Domain.Promotions;

namespace OverCloudAirways.PaymentService.Application.Orders.Commands.ApplyDiscount;

public record ApplyOrderDiscountCommand(
    OrderId OrderId,
    PromotionId PromotionId) : Command;
