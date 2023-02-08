using OverCloudAirways.BuildingBlocks.Application.Queries;

namespace OverCloudAirways.BookingService.Application.Customers.Queries.GetInfo;

public record GetCustomerInfoQuery(Guid CustomerId) : Query<CustomerDto>;
