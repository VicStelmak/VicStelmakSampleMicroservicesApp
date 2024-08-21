using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Domain.Models;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    internal interface IOrderRepository
    {
        Task AddLineItemToOrder(LineItemModel item);
        Task CreateOrder(DeliveryAddressModel address, OrderModel order, int productId);
        Task DeleteOrder(int orderId);
        Task<OrderModel> FindOrderByUserEmailAsync(string orderStatus, string userEmail);
        Task<List<LineItemModel>> GetLineItemsByOrderIdAsync(int orderId);
        Task<OrderModel> GetOrderByIdAsync(int orderId, string orderStatus);
        Task<List<OrderModel>> GetOrdersList();
        Task UpdateOrder(OrderModel order);
    }
}
