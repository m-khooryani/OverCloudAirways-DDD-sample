using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Domain.Products;

namespace OverCloudAirways.PaymentService.Application.Products.Commands.Create;

internal class CreateProductCommandHandler : CommandHandler<CreateProductCommand>
{
    private readonly IAggregateRepository _aggregateRepository;

    public CreateProductCommandHandler(IAggregateRepository aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
    }

    public override Task HandleAsync(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = Product.Create(
            command.ProductId,
            command.Name,
            command.Description,
            command.Price);

        _aggregateRepository.Add(product);

        return Task.CompletedTask;
    }
}