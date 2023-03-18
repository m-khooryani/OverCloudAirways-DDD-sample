using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.CrmService.Domain.Customers;

namespace OverCloudAirways.CrmService.Application.Customers.Commands.ResetLoyaltyPoints;

internal class ResetCustomerLoyaltyPointsCommandHandler : CommandHandler<ResetCustomerLoyaltyPointsCommand>
{
    private readonly IAggregateRepository _repository;

    public ResetCustomerLoyaltyPointsCommandHandler(IAggregateRepository repository)
    {
        _repository = repository;
    }

    public override async Task HandleAsync(ResetCustomerLoyaltyPointsCommand command, CancellationToken cancellationToken)
    {
        var customer = await _repository.LoadAsync<Customer, CustomerId>(command.CustomerId);
        customer.ResetLoyaltyPoints();
    }
}