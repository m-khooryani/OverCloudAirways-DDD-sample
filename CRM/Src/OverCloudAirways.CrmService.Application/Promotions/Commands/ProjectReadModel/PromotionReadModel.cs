﻿using OverCloudAirways.BuildingBlocks.Application.Models;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;

namespace OverCloudAirways.CrmService.Application.Promotions.Commands.ProjectReadModel;

internal record PromotionReadModel(
    Guid PromotionId,
    string DiscountCode,
    Percentage DiscountPercentage,
    string? Description,
    DateTimeOffset ExpirationDate,
    string? CustomerFirstName,
    string? CustomerLastName) : ReadModel(DiscountCode, "Promotion");