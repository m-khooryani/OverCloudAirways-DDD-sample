using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.PaymentService.Domain.Orders;

namespace OverCloudAirways.PaymentService.IntegrationEvents.Orders;

public record OrderExpiredIntegrationEvent(OrderId OrderId)
    : IntegrationEvent(OrderId, "payment-order-expired");
