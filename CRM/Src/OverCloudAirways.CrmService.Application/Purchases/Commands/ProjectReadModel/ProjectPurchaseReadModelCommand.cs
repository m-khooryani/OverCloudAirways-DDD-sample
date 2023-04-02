using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.CrmService.Domain.Purchases;

namespace OverCloudAirways.CrmService.Application.Purchases.Commands.ProjectReadModel;

public record ProjectPurchaseReadModelCommand(PurchaseId PurchaseId) : Command;
