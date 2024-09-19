using VicStelmak.Sma.WebUiDataLibrary.Order.Requests;
using VicStelmak.Sma.WebUiDataLibrary.Order.Responses;

namespace VicStelmak.Sma.WebUiDataLibrary.Order
{
    public interface IOrderService
    {
        Task CreateLineItemAsync(CreateLineItemRequest request);
        Task<bool> CheckIfPendingOrderExistsAsync(string userEmail);
        Task CreateOrderAsync(CreateOrderRequest request);
        Task DeleteOrderAsync(string deletedBy, int orderId);
        Task<List<GetOrderResponse>> FindOrdersByUserEmailAsync(string userEmail);
        Task<FindPendingOrderResponse> FindPendingOrderByUserEmailAsync(string userEmail);
        Task<List<GetLineItemsResponse>> GetLineItemsByOrderIdAsync(int orderId);
        Task<GetOrderResponse> GetOrderByIdAsync(int orderId);
        Task<List<GetOrderResponse>> GetOrdersAsync();
        Task SendOrderSubmittingEventAsync(SendOrderSubmittingEventRequest request);
        Task UpdateOrderAsync(int orderId, UpdateOrderRequest request);
    }
}