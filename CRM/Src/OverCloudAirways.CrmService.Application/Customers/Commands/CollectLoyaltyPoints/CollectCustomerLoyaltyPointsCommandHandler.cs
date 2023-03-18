using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.CrmService.Domain.Customers;

namespace OverCloudAirways.CrmService.Application.Customers.Commands.CollectLoyaltyPoints;

internal class CollectCustomerLoyaltyPointsCommandHandler : CommandHandler<CollectCustomerLoyaltyPointsCommand>
{
    private readonly IAggregateRepository _repository;

    public CollectCustomerLoyaltyPointsCommandHandler(IAggregateRepository repository)
    {
        _repository = repository;
    }

    public override async Task HandleAsync(CollectCustomerLoyaltyPointsCommand command, CancellationToken cancellationToken)
    {
        var customer = await _repository.LoadAsync<Customer, CustomerId>(command.CustomerId);
        customer.CollectLoyaltyPoints(command.LoyaltyPoints);
    }
}