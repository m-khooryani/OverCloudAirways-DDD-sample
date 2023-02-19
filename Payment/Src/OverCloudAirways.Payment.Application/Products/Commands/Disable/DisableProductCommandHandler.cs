using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Domain.Products;

namespace OverCloudAirways.PaymentService.Application.Products.Commands.Disable;

internal class DisableProductCommandHandler : CommandHandler<DisableProductCommand>
{
    private readonly IAggregateRepository _aggregateRepository;

    public DisableProductCommandHandler(IAggregateRepository aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
    }

    public override async Task HandleAsync(DisableProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _aggregateRepository.LoadAsync<Product, ProductId>(command.ProductId);

        product.Disable();
    }
}
