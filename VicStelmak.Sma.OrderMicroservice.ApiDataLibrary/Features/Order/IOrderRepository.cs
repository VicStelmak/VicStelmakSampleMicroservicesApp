using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Domain.Models;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order
{
    internal interface IOrderRepository
    {
        Task CreateOrder(DeliveryAddressModel address, OrderModel order);
        Task DeleteOrder(int orderId);
        Task<OrderModel> GetOrderByIdAsync(int id);
        Task<List<OrderModel>> GetOrdersList();
        Task UpdateOrder(OrderModel order);
    }
}
