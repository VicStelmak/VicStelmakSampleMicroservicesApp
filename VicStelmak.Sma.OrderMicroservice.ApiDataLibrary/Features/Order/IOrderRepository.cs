using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Domain.Models;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    internal interface IOrderRepository
    {
        Task CreateLineItemAsync(LineItemModel item);
        Task CreateOrderAsync(OrderModel order, int productId, decimal lineItemTotalPrice);
        Task DeleteOrderAsync(int orderId);
        Task<List<OrderModel>> FindOrdersByUserEmailAsync(string userEmail);
        Task<OrderModel> FindPendingOrderByUserEmailAsync(string userEmail);
        Task<List<LineItemModel>> GetLineItemsByOrderIdAsync(int orderId);
        Task<OrderModel> GetOrderByIdAsync(int orderId);
        Task<List<OrderModel>> GetOrdersAsync();
        Task UpdateOrderAsync(OrderModel order);
    }
}
