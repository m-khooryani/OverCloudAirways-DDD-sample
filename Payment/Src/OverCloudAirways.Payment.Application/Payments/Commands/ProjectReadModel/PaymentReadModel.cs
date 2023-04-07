using OverCloudAirways.BuildingBlocks.Application.Models;
using OverCloudAirways.PaymentService.Domain.Payments;

namespace OverCloudAirways.PaymentService.Application.Payments.Commands.ProjectReadModel;

internal record PaymentReadModel(
    Guid PaymentId,
    decimal Amount,
    decimal InvoiceAmount,
    PaymentMethod Method,
    string ReferenceNumber) : ReadModel(PaymentId.ToString(), PaymentId.ToString());
