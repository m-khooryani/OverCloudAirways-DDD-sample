using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.PaymentService.Domain.Payments;

namespace OverCloudAirways.PaymentService.Application.Payments.Commands.ProjectReadModel;

public record ProjectPaymentReadModelCommand(PaymentId PaymentId) : Command;
