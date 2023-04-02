namespace OverCloudAirways.CrmService.Application.Purchases.Queries.GetInfo;

public record PurchaseDto(
    Guid PurchaseId,
    Guid CustomerId,
    string CustomerFirstName,
    string CustomerLastName,
    DateTimeOffset Date,
    decimal Amount);