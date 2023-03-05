using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;
using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Orders;
using OverCloudAirways.PaymentService.Domain.Products;

namespace OverCloudAirways.PaymentService.Application.Orders.Commands.ProjectReadModel;

internal class ProjectOrderReadModelCommandHandler : CommandHandler<ProjectOrderReadModelCommand>
{
    private readonly ICosmosManager _cosmosManager;
    private readonly IAggregateRepository _aggregateRepository;

    public ProjectOrderReadModelCommandHandler(
        ICosmosManager cosmosManager,
        IAggregateRepository aggregateRepository)
    {
        _cosmosManager = cosmosManager;
        _aggregateRepository = aggregateRepository;
    }

    public override async Task HandleAsync(ProjectOrderReadModelCommand command, CancellationToken cancellationToken)
    {
        var order = await _aggregateRepository.LoadAsync<Order, OrderId>(command.OrderId);
        var buyer = await _aggregateRepository.LoadAsync<Buyer, BuyerId>(order.BuyerId);
        var orderItems = await GetOrderItems(order);

        var readmodel = new OrderReadModel(
            order.Id.Value,
            buyer.FirstName,
            buyer.LastName,
            order.Date,
            order.TotalAmount,
            orderItems);

        await _cosmosManager.UpsertAsync(ContainersConstants.ReadModels, readmodel);
    }

    private async Task<IReadOnlyList<OrderItemReadModel>> GetOrderItems(Order order)
    {
        var orderItems = new List<OrderItemReadModel>();
        foreach (var orderItem in order.OrderItems)
        {
            var product = await _aggregateRepository.LoadAsync<Product, ProductId>(orderItem.ProductId);
            orderItems.Add(new OrderItemReadModel(product.Name, orderItem.UnitPrice, orderItem.Quantity, orderItem.TotalPrice));
        }

        return orderItems;
    }
}
