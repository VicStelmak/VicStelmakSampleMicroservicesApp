using VicStelmak.Sma.WebUiDataLibrary.Order.Requests;
using VicStelmak.Sma.WebUiDataLibrary.Order.Responses;

namespace VicStelmak.Sma.WebUiDataLibrary.Order
{
    public interface IOrderService
    {
        Task AddLineItemToOrderAsync(AddLineItemToOrderRequest request);
        Task<bool> CheckIfOrderExistsAsync(string orderStatus, string userEmail);
        Task CreateOrderAsync(CreateOrderRequest request);
        Task<FindOrderResponse> FindOrderByUserEmailAsync(string orderStatus, string userEmail);
        Task<GetOrderResponse> GetOrderByIdAsync(int orderId, string orderStatus);
        Task UpdateOrderAsync(int orderId, UpdateOrderRequest request);
    }
}