using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Domain.Models;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    internal interface IOrderRepository
    {
        Task AddLineItemToOrderAsync(LineItemModel item);
        Task CreateOrderAsync(DeliveryAddressModel address, OrderModel order, int productId);
        Task DeleteOrderAsync(int orderId);
        Task<OrderModel> FindPendingOrderByUserEmailAsync(string userEmail);
        Task<List<LineItemModel>> GetLineItemsByOrderIdAsync(int orderId);
        Task<OrderModel> GetOrderByIdAsync(int orderId);
        Task<List<OrderModel>> GetOrdersList();
        Task UpdateOrderAsync(OrderModel order);
    }
}
