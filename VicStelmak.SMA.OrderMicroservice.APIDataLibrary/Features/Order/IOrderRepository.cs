using VicStelmak.Sma.OrderMicroservice.APIDataLibrary.Domain.Models;

namespace VicStelmak.Sma.OrderMicroservice.APIDataLibrary.Features.Order
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
