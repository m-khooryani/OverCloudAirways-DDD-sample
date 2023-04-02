using OverCloudAirways.BuildingBlocks.Application.Queries;
using OverCloudAirways.CrmService.Domain.Customers;
using OverCloudAirways.CrmService.Domain.Purchases;

namespace OverCloudAirways.CrmService.Application.Purchases.Queries.GetInfo;

public record GetPurchaseInfoQuery(
    PurchaseId PurchaseId,
    CustomerId CustomerId) : Query<PurchaseDto>;
