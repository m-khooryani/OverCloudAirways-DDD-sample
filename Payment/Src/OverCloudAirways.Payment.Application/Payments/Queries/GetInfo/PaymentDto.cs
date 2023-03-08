using OverCloudAirways.PaymentService.Domain.Payments;

namespace OverCloudAirways.PaymentService.Application.Payments.Queries.GetInfo;

public record PaymentDto(
    Guid Id,
    decimal Amount,
    decimal InvoiceAmount,
    PaymentMethod Method,
    string ReferenceNumber);
