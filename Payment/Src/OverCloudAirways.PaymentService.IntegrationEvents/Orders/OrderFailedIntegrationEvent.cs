using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.PaymentService.Domain.Orders;

namespace OverCloudAirways.PaymentService.IntegrationEvents.Orders;

public record OrderFailedIntegrationEvent(OrderId OrderId)
    : IntegrationEvent(OrderId, "payment-order-failed");
