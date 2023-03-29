using System.Collections.ObjectModel;
using OverCloudAirways.PaymentService.Domain.Orders;

namespace OverCloudAirways.PaymentService.Domain.Products;

public interface IConnectedOrders
{
    Task<ReadOnlyCollection<OrderId>> GetConnectedOrderIds(ProductId productId);
}