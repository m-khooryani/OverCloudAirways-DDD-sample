using OverCloudAirways.BuildingBlocks.Application.Models;
using OverCloudAirways.PaymentService.Domain.Payments;

namespace OverCloudAirways.PaymentService.Application.Payments.Commands.ProjectReadModel;

internal record PaymentReadModel(
    Guid Id,
    decimal Amount,
    decimal InvoiceAmount,
    PaymentMethod Method,
    string ReferenceNumber) : ReadModel(Id.ToString(), Id.ToString());
