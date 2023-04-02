using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.CrmService.Domain.Customers;
using OverCloudAirways.CrmService.Domain.Purchases;

namespace OverCloudAirways.CrmService.Application.Purchases.Commands.Make;

public record MakePurchaseCommand(
    PurchaseId PurchaseId,
    CustomerId CustomerId,
    decimal Amount) : Command;
