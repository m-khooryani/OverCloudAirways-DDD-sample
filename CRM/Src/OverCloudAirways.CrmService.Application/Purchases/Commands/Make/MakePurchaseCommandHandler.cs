using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.CrmService.Domain.Purchases;

namespace OverCloudAirways.CrmService.Application.Purchases.Commands.Make;

internal class MakePurchaseCommandHandler : CommandHandler<MakePurchaseCommand>
{
    private readonly IAggregateRepository _repository;

    public MakePurchaseCommandHandler(IAggregateRepository repository)
    {
        _repository = repository;
    }

    public override Task HandleAsync(MakePurchaseCommand command, CancellationToken cancellationToken)
    {
        var purchase = Purchase.Make(command.PurchaseId, command.CustomerId, command.Amount);

        _repository.Add(purchase);

        return Task.CompletedTask;
    }
}