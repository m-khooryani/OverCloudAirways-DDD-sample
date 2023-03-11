using OverCloudAirways.BuildingBlocks.Application.Queries;

namespace OverCloudAirways.CrmService.Application.Customers.Queries.GetInfo;

public record GetCustomerInfoQuery(Guid CustomerId) : Query<CustomerDto>;
