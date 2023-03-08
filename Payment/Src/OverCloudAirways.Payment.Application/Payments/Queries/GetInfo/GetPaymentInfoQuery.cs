using OverCloudAirways.BuildingBlocks.Application.Queries;

namespace OverCloudAirways.PaymentService.Application.Payments.Queries.GetInfo;

public record GetPaymentInfoQuery(Guid PaymentId) : Query<PaymentDto>;
