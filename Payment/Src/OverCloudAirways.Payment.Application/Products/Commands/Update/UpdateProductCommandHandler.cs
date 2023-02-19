using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Domain.Products;

namespace OverCloudAirways.PaymentService.Application.Products.Commands.Update;

internal class UpdateProductCommandHandler : CommandHandler<UpdateProductCommand>
{
    private readonly IAggregateRepository _aggregateRepository;

    public UpdateProductCommandHandler(IAggregateRepository aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
    }

    public override async Task HandleAsync(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _aggregateRepository.LoadAsync<Product, ProductId>(command.ProductId);

        product.Update(command.Name, command.Description, command.Price);
    }
}